using _911Medical.Application.Features.TripFeatures.Commands;
using _911Medical.Application.Features.TripFeatures.Events;
using _911Medical.Application.Services.TripServices;
using _911Medical.Domain.Entities;
using _911Medical.Domain.Interfaces;
using _911Medical.Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.TripFeatures.Handlers
{
    public class DispatchTripCommandHandler : IRequestHandler<DispatchTripCommand, Trip>
    {
        private readonly IRepository<Trip> _tripRepository;
        private readonly IReadRepository<Vehicle> _vehicleRepository;
        private readonly FindClosestVehicleService _closestService;
        private readonly IMediator _mediator;

        public DispatchTripCommandHandler(
            IRepository<Trip> tripRepository, 
            IReadRepository<Vehicle> vehicleRepository, 
            FindClosestVehicleService closestService, 
            IMediator mediator)
        {
            this._tripRepository = tripRepository;
            this._vehicleRepository = vehicleRepository;
            this._closestService = closestService;
            this._mediator = mediator;
        }

        public async Task<Trip> Handle(DispatchTripCommand request, CancellationToken cancellationToken)
        {
            // get trip entity
            var trip = await this._tripRepository.GetBySpecAsync(new TripByIdSpec(request.TripId), cancellationToken);

            if (trip == null)
            {
                throw new ArgumentOutOfRangeException(nameof(request.TripId), $"Trip with id={request.TripId} was not found.");
            }

            // get available vehicles
            var allVehiclesSpec = new VehiclesByStatusSpec(null); 
            var connectedVehiclesSpec = new VehiclesByStatusSpec(VehicleStatus.Connected);

            // TODO: connectedVehiclesSpec should be used after vehicle status is properly implemented
            var vehicles = await this._vehicleRepository.ListAsync(allVehiclesSpec, cancellationToken);

            if (!vehicles.Any())
            {
                throw new ApplicationException("No vehicles are available to accept the trip.");
            }

            // find closest vehicle
            var vehicle = this._closestService.Find(vehicles, trip.StartAddress);

            // assign vehicle to the trip
            trip.AssignVehicle(vehicle);

            // persist state
            await this._tripRepository.UpdateAsync(trip);

            //TODO: Send notification via SignalR hub
            await _mediator.Publish(new TripDispatchedEvent() { TripId = trip.Id, VehicleId = vehicle.Id }, cancellationToken);

            return trip;
        }
    }
}
