using _911Medical.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Entities
{
    public class Vehicle : BaseEntity, IAggregateRoot
    {
        public string RegNumber { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public string Description { get; private set; }
        public VehicleState State { get; private set; }

        private Vehicle() { }

        public static Vehicle CreateVehicle(string regNumber, VehicleType vehicleType, string description)
        {
            var vehicle = new Vehicle()
            {
                RegNumber = regNumber,
                VehicleType = vehicleType,
                Description = description
            };

            return vehicle;
        }

        public void SetRegNumber(string regNumber)
        {
            this.RegNumber = regNumber;
        }

        public void SetVehicleType(VehicleType vehicleType)
        {
            this.VehicleType = vehicleType;
        }

        public void SetDescription(string description)
        {
            this.Description = description;
        }

        public void SetState(VehicleState state)
        {
            //state.Vehicle = this;
            //state.VehicleId = this.Id;
            this.State = state;
        }
    }
}
