using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _911Medical.Domain.Entities;
using Ardalis.Specification;

namespace _911Medical.Domain.Specifications
{
    public class TripByIdSpec : BaseTripSpec, ISingleResultSpecification<Trip>
    {
        public TripByIdSpec(int id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
