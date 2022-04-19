using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace _911Medical.WebApp.Models
{
    public class TripAssignViewModel : TripViewModel
    {
        public SelectList AvailableVehicles { get; set; }
    }
}
