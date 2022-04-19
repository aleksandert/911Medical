using _911Medical.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Entities
{
    /// <summary>
    /// Represents a Trip in different states.
    /// </summary>
    public class Trip : BaseEntity, IAggregateRoot
    {
        public Vehicle Vehicle { get; private set; }

        public Patient Patient { get; private set; }

        public TripStatus Status { get; private set; }

        public DateTime DateCreated { get; private set; }

        public Address StartAddress { get; private set; }


        // Do not allow application to create new instances (also required by EF).
        private Trip() { }

        
        /// <summary>
        /// Creates and enqueues new Trip for given Patient.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Trip EnqueueNewTrip(Patient patient)
        {
            // Entities should be instanced with "bussines context aware" factory methods and not using public constructors.
            // This is because we want our methods to speak the domain language and implement exact bussines requirements/features.
            // Eg: This factory method is gonna be only used when new trip request is being processed, so 
            // we know the status of new trip should be set to Queued and that vehicle is gonna get assigned later in the process.
            
            return new Trip()
            {
                Patient = patient ?? throw new ArgumentNullException(nameof(patient)),
                // Patients HomeAddress is copied over.
                StartAddress = patient.HomeAddress ?? throw new ArgumentNullException(nameof(patient.HomeAddress)),
                Status = TripStatus.Queued,
                DateCreated = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Assigns Vehicle to this trip and sets the trip status to Assigned.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AssignVehicle(Vehicle vehicle)
        {
            this.Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));

            UpdateTripStatus(TripStatus.Assigned);
        }

        // This method is not DDD friendly, but left there as an example to explain why is that. :)
        // Btw, could still be useful as a helper method within this ddd entity (private scope)
        public void UpdateTripStatus(TripStatus tripStatus)
        {
            //TODO: we should check if status transition is actually valid...

            this.Status = tripStatus;
        }
    }
}
