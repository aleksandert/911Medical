using _911Medical.Domain.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Specifications
{
    public class TripsByStatusSpec : BaseTripSpec
    {
        public TripsByStatusSpec(TripStatus? tripStatus)
        {
            if (tripStatus != null)
            {
                Query.Where(x => x.Status == tripStatus);
            }

            Query.AsNoTracking();
        }
    }
}
