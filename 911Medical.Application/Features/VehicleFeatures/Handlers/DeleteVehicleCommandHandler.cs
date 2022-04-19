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
    /// <summary>
    /// Handles DeleteVehicleCommand
    /// </summary>
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly IRepository<Vehicle> _repository;

        public DeleteVehicleCommandHandler(IRepository<Vehicle> repository)
        {
            this._repository = repository;
        }

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            //load vehicle instance from repository
            var vehicle = await this._repository.GetByIdAsync(request.Id);

            if (vehicle == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            //delete from repository
            await this._repository.DeleteAsync(vehicle, cancellationToken);

            return Unit.Value;
        }
    }
}
