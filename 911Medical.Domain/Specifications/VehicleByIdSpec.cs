using _911Medical.Domain.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Specifications
{
    public class VehicleByIdSpec : Specification<Vehicle>, ISingleResultSpecification
    {
        public VehicleByIdSpec(int id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
