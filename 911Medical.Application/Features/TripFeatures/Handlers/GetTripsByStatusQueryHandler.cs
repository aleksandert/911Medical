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

namespace _911Medical.Application.Features.TripFeatures.Handlers
{
    public class GetTripsByStatusQueryHandler : IRequestHandler<GetTripsByStatusQuery, IEnumerable<Trip>>
    {
        private readonly IReadRepository<Trip> _repository;

        public GetTripsByStatusQueryHandler(IReadRepository<Trip> repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<Trip>> Handle(GetTripsByStatusQuery request, CancellationToken cancellationToken)
        {
            var spec = new TripsByStatusSpec(request.TripStatus);
            
            return await this._repository.ListAsync(spec, cancellationToken);
        }
    }
}
