using _911Medical.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.VehicleFeatures.Commands
{
    /// <summary>
    /// Payload for command that creates new vehicle entity.
    /// </summary>
    public class CreateVehicleCommand : IRequest<int>
    {
        /// <summary>
        /// Registration number
        /// </summary>
        public string RegNumber { get; set; }
        
        /// <summary>
        /// Type of the vehicle
        /// </summary>
        public VehicleType VehicleType { get; set; }
        
        /// <summary>
        /// Description of the vehicle
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Validates payload for create vehicle command.
    /// </summary>
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.RegNumber)
                .NotEmpty()
                .MaximumLength(15)
                .Matches("^[A-Z]{2}\\s?.{3,15}$"); //needs to start with two uppercase letters, then optional whitespace and at least three characters

            RuleFor(x => x.VehicleType)
                .IsInEnum(); // ensure it's defined in VehicleType enum

            RuleFor(x => x.Description)
                .MaximumLength(255); 

        }
    }
}
