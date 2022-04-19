using _911Medical.Domain.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Specifications
{
    public class VehiclesByStatusSpec : Specification<Vehicle>
    {
        public VehiclesByStatusSpec(VehicleStatus? status)
        {
            Query.Include(i => i.State);

            if (status != null)
            {
                Query
                    .Where(x => x.State != null)
                    .Where(x => x.State.Status == status);

            }
        }
    }
}
