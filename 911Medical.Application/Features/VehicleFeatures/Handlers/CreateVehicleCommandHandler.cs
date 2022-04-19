using _911Medical.Application.Features.VehicleFeatures.Commands;
using _911Medical.Domain.Entities;
using _911Medical.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.VehicleFeatures.Handlers
{
    /// <summary>
    /// Handles CreateVehicleCommand 
    /// </summary>
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
    {
        private readonly IRepository<Vehicle> _repository;

        public CreateVehicleCommandHandler(IRepository<Vehicle> repository)
        {
            this._repository = repository;
        }

        public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = Vehicle.CreateVehicle(request.RegNumber, request.VehicleType, request.Description);

            // persist new instance
            vehicle = await this._repository.AddAsync(vehicle, cancellationToken);

            return vehicle.Id;
        }
    }
}
