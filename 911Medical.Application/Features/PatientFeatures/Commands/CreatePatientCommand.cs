using _911Medical.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.PatientFeatures.Commands
{
    public class CreatePatientCommand : IRequest<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address HomeAddress { get; set; }
    }

    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
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
                .MaximumLength(30);

            RuleFor(x => x.HomeAddress.CountryIso)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
