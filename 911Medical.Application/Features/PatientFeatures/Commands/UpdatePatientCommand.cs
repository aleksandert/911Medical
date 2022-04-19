using _911Medical.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.PatientFeatures.Commands
{
    public class UpdatePatientCommand : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address HomeAddress { get; set; }
    }

    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.HomeAddress)
                .NotNull();

            RuleFor(x => x.HomeAddress.AddressLine1)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.HomeAddress.AddressLine2)
                .MaximumLength(100);

            RuleFor(x => x.HomeAddress.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.HomeAddress.ZipPostalCode)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.HomeAddress.ProvinceStateRegionCode)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.HomeAddress.CountryIso)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
