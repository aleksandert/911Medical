using _911Medical.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.TripFeatures.Queries
{
    public class GetTripsByStatusQuery : IRequest<IEnumerable<Trip>>
    {
        // When null, results should not be filtered by status
        public TripStatus? TripStatus { get; set; }
    }
}
