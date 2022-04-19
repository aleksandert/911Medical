using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.TripFeatures.Events
{
    public class TripDispatchedEvent : INotification
    {
        public int TripId { get; set; }
        public int VehicleId { get; set; }
    }
}
