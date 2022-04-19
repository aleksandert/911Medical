using _911Medical.Application.Features.VehicleFeatures.Queries;
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
    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, IEnumerable<Vehicle>>
    {
        private readonly IReadRepository<Vehicle> _repository;

        public GetAllVehiclesQueryHandler(IReadRepository<Vehicle> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await this._repository.ListAsync(cancellationToken);

            return vehicles;
        }
    }
}
