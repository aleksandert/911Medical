using _911Medical.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.TripFeatures.Commands
{
    public class EnqueueNewTripCommand : IRequest<Trip>
    {
        public int PatientId { get; set; }
    }
}
