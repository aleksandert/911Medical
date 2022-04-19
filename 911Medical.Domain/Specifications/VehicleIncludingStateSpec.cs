using _911Medical.Domain.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Specifications
{
    public class VehicleIncludingStateSpec : Specification<Vehicle>
    {
        public VehicleIncludingStateSpec()
        {
            Query.Include(x => x.State);
        }
    }
}
