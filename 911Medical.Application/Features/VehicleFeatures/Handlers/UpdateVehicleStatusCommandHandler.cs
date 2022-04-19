using _911Medical.Application.Features.VehicleFeatures.Commands;
using _911Medical.Domain.Entities;
using _911Medical.Domain.Interfaces;
using _911Medical.Domain.Specifications;
using Ardalis.Specification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.VehicleFeatures.Handlers
{
   
    public class UpdateVehicleStatusCommandHandler : IRequestHandler<UpdateVehicleStatusCommand>
    {
        private readonly IRepository<Vehicle> _repository;

        public UpdateVehicleStatusCommandHandler(IRepository<Vehicle> repository)
        {
            this._repository = repository;
        }

        public async Task<Unit> Handle(UpdateVehicleStatusCommand request, CancellationToken cancellationToken)
        {

            var spec = new VehicleByIdSpec(request.VehicleId)
                        .Combine(new VehicleIncludingStateSpec());
            
            var vehicle = await this._repository.GetBySpecAsync(spec);

            if (vehicle == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (vehicle.State == null)
            {
                vehicle.SetState(new VehicleState()
                {
                    Status = request.Status,
                    CurrentCity = request.CurrentCity,
                });
            }
            else
            {
                vehicle.State.Status = request.Status;
                vehicle.State.CurrentCity = request.CurrentCity;
            }

            await this._repository.UpdateAsync(vehicle, cancellationToken);

            return Unit.Value;
        }
    }
}
