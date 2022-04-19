using _911Medical.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.TripFeatures.Commands
{
    public class DispatchTripCommand : IRequest<Trip>
    {
        public int TripId { get; set; }
    }
}
