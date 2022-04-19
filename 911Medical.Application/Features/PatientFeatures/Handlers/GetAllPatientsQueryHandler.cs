using _911Medical.Application.Features.PatientFeatures.Queries;
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
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<Patient>>
    {
        private readonly IReadRepository<Patient> _repository;

        public GetAllPatientsQueryHandler(IReadRepository<Patient> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Patient>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.ListAsync(cancellationToken);
        }
    }
}
