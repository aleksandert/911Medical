using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Entities
{
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string City { get; set; }
        public string ProvinceStateRegionCode { get; set; }
        /// <summary>
        /// ISO 3166-1 Alpha 2 code (https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2#Officially_assigned_code_elements)
        /// </summary>
        public string CountryIso { get; set; }

        private Address() { }

        public Address(string addressLine1, string addressLine2, string zipPostalCode, string city, string provinceStateRegionCode, string countryIso)
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.City = city;
            this.CountryIso = countryIso;
            this.ProvinceStateRegionCode = provinceStateRegionCode;
            this.ZipPostalCode = zipPostalCode;
        }
    }
}
