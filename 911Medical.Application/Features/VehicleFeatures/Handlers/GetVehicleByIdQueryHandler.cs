using _911Medical.Application.Features.VehicleFeatures.Queries;
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

namespace _911Medical.Application.Features.VehicleFeatures.Handlers
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Vehicle>
    {
        private readonly IReadRepository<Vehicle> _repository;

        public GetVehicleByIdQueryHandler(IReadRepository<Vehicle> repository)
        {
            this._repository = repository;
        }

        public async Task<Vehicle> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {

            var spec = new VehicleByIdSpec(request.Id);

            var vehicle = await this._repository.GetBySpecAsync(spec, cancellationToken);

            return vehicle;
        }
    }
}
