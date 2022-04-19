using _911Medical.Application.Features.PatientFeatures.Commands;
using _911Medical.Domain.Entities;
using _911Medical.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.PatientFeatures.Handlers
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
    {
        private readonly IRepository<Patient> _repository;

        public CreatePatientCommandHandler(IRepository<Patient> repository)
        {
            this._repository = repository;
        }

        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = new Patient(request.FirstName, request.LastName, request.HomeAddress);

            patient = await this._repository.AddAsync(patient, cancellationToken);

            return patient.Id;
        }
    }
}
