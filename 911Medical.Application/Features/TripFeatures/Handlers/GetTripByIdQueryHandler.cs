using _911Medical.Application.Features.TripFeatures.Queries;
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
    public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, Trip>
    {
        private readonly IReadRepository<Trip> _repository;

        public GetTripByIdQueryHandler(IReadRepository<Trip> repository)
        {
            this._repository = repository;
        }

        public async Task<Trip> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.GetBySpecAsync(new TripByIdSpec(request.Id), cancellationToken);
        }
    }
}
