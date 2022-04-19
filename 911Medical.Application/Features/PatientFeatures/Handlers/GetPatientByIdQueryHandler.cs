using _911Medical.Application.Features.PatientFeatures.Queries;
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

namespace _911Medical.Application.Features.PatientFeatures.Handlers
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Patient>
    {
        private readonly IReadRepository<Patient> _repository;

        public GetPatientByIdQueryHandler(IReadRepository<Patient> repository)
        {
            this._repository = repository;
        }

        public async Task<Patient> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new PatientByIdSpec(request.Id);

            return await this._repository.GetBySpecAsync(spec, cancellationToken);
        }
    }
}
