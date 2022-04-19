using _911Medical.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.VehicleFeatures.Commands
{
    public class UpdateVehicleStatusCommand : IRequest
    {
        public int VehicleId { get; set; }
        public string User { get; set; }
        public string CurrentCity { get; set; }
        public VehicleStatus Status { get; set; }
    }
}
