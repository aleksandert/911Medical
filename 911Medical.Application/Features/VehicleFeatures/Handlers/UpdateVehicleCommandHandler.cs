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
   
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>
    {
        private readonly IRepository<Vehicle> _repository;

        public UpdateVehicleCommandHandler(IRepository<Vehicle> repository)
        {
            this._repository = repository;
        }

        public async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await this._repository.GetByIdAsync(request.Id);

            if (vehicle == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            vehicle.SetRegNumber(request.RegNumber);
            vehicle.SetVehicleType(request.VehicleType);
            vehicle.SetDescription(request.Description);

            await this._repository.UpdateAsync(vehicle, cancellationToken);

            return Unit.Value;
        }
    }
}
