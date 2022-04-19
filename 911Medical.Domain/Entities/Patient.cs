using _911Medical.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Entities
{
    public class Patient : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address HomeAddress { get; private set; }

        public Patient(string firstName, string lastName, Address homeAddress)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.HomeAddress = homeAddress;
        }

        private Patient() { }
    }
}
