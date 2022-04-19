using _911Medical.Domain.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Specifications
{
    public abstract class BaseTripSpec : Specification<Trip>
    {
        public BaseTripSpec()
        {
            Query.Include(x => x.Patient);
            Query.Include(x => x.Vehicle);
        }
    }
}
