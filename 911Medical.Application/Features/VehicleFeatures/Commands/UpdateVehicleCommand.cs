using _911Medical.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.VehicleFeatures.Commands
{
    /// <summary>
    /// Payload for command that updates vehicle entity.
    /// </summary>
    public class UpdateVehicleCommand : IRequest
    {
        /// <summary>
        /// Id of the vehicle to update.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Registration number.
        /// </summary>
        public string RegNumber { get; set; }
        
        /// <summary>
        /// Type of the vehicle.
        /// </summary>
        public VehicleType VehicleType { get; set; }
        
        /// <summary>
        /// Description of the vehicle.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Validators associated with UpdateVehicleCommand payload.
    /// </summary>
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(x => x.RegNumber)
                .NotEmpty()
                .MaximumLength(15)
                .Matches("^[A-Z]{2}\\s?.{3,15}$");

            RuleFor(x => x.VehicleType)
                .IsInEnum();

            RuleFor(x => x.Description)
                .MaximumLength(255);
        }
    }
}
