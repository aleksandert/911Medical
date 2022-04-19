using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Entities
{
    public class VehicleState 
    {
       
        public VehicleStatus Status { get; set; }

        public string CurrentCity { get; set; }
        [Key]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
