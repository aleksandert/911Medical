using _911Medical.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace _911Medical.WebApp.Models
{
    public class TripViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Create date")]
        public DateTime DateCreated { get; set; }

        public TripStatus Status { get; set; }

        [Display(Name = "Trip status")]
        public string TripStatusName { get; set; }
        public int PatientId { get; set; }
        
        [Display(Name = "Patient")]
        public string PatientFullName { get; set; }

        [Display(Name = "Start address")]
        public string StartAddress { get; set; }
        public int? VehicleId { get; set; }

        [Display(Name = "Vehicle")]
        public string VehicleRegNumber { get; set; }
    }
}
