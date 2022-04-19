using Xunit;
using _911Medical.Application.Features.VehicleFeatures.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _911Medical.Domain.Entities;
using Xunit.Abstractions;

namespace _911Medical.Application.Features.VehicleFeatures.Commands.Tests
{
    public class CreateVehicleCommandValidatorTests
    {
        private readonly ITestOutputHelper output;

        public CreateVehicleCommandValidatorTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("123", false)]
        [InlineData("01234567890123456", false)]
        [InlineData("MB ZU589ZU589ZU589", false)]
        [InlineData("MB ZU589", true)]
        [InlineData("MB ZU589 ZU589", true)]
        public async Task CreateVehicleCommandValidatorTest_RegNumber(string regNumber, bool expectedIsValid)
        {
            var command = new CreateVehicleCommand()
            {
                RegNumber = regNumber,
                VehicleType = Domain.Entities.VehicleType.Personal,
                Description = "Description"
            };

            var validationResults = await new CreateVehicleCommandValidator().ValidateAsync(command);

            validationResults.Errors.ForEach(x => output.WriteLine(x.ErrorMessage));

            Assert.Equal(expectedIsValid, validationResults.IsValid);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData((int)VehicleType.Personal, true)]
        [InlineData((int)VehicleType.Van, true)]
        [InlineData(10, false)]
        public async Task CreateVehicleCommandValidatorTest_VehicleType(int vehicleTypeEnumValue, bool expectedIsValid)
        {
            var command = new CreateVehicleCommand()
            {
                RegNumber = "MB ZU589",
                VehicleType = (VehicleType)vehicleTypeEnumValue,
                Description = "Description"
            };

            var validationResults = await new CreateVehicleCommandValidator().ValidateAsync(command);

            validationResults.Errors.ForEach(x => output.WriteLine(x.ErrorMessage));

            Assert.Equal(expectedIsValid, validationResults.IsValid);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("Description", true)]
        public async Task CreateVehicleCommandValidatorTest_Description(string description, bool expectedIsValid)
        {
            var command = new CreateVehicleCommand()
            {
                RegNumber = "MB ZU589",
                VehicleType =  VehicleType.Personal,
                Description = description
            };

            var validationResults = await new CreateVehicleCommandValidator().ValidateAsync(command);

            validationResults.Errors.ForEach(x => output.WriteLine(x.ErrorMessage));

            Assert.Equal(expectedIsValid, validationResults.IsValid);
        }

        [Fact()]
        public async Task CreateVehicleCommandValidatorTest_Description_To_Long()
        {
            var command = new CreateVehicleCommand()
            {
                RegNumber = "MB ZU589",
                VehicleType = VehicleType.Personal,
                Description = String.Join("", Enumerable.Repeat("12345678", 32))
            };

            var validationResults = await new CreateVehicleCommandValidator().ValidateAsync(command);

            validationResults.Errors.ForEach(x => output.WriteLine(x.ErrorMessage));

            Assert.False(validationResults.IsValid, "Validation result should be false.");
        }
    }
}