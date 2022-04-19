using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.VehicleFeatures.Commands
{
    /// <summary>
    /// Payload for command that deletes vehicle.
    /// </summary>
    public class DeleteVehicleCommand : IRequest
    {
        /// <summary>
        /// Id of the vehicle to be deleted.
        /// </summary>
        public int Id { get; set; }
    }
}
