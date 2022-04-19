using _911Medical.Application.Features.TripFeatures.Commands;
using _911Medical.Domain.Entities;
using _911Medical.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.TripFeatures.Handlers
{
    public class EnqueueNewTripCommandHandler : IRequestHandler<EnqueueNewTripCommand, Trip>
    {
        private readonly IRepository<Trip> _tripRepository;
        private readonly IReadRepository<Patient> _patientRepository;

        public EnqueueNewTripCommandHandler(IRepository<Trip> tripRepository, IReadRepository<Patient> patientRepository)
        {
            this._tripRepository = tripRepository;
            this._patientRepository = patientRepository;
        }

        public async Task<Trip> Handle(EnqueueNewTripCommand request, CancellationToken cancellationToken)
        {
            // get patient entity
            var patient = await this._patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
            {
                throw new ArgumentOutOfRangeException(nameof(request.PatientId));
            }

            // create new trip and persist it
            var trip = Trip.EnqueueNewTrip(patient);

            return await this._tripRepository.AddAsync(trip);
        }
    }
}
