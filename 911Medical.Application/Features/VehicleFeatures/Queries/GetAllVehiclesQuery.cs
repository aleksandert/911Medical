using _911Medical.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.VehicleFeatures.Queries
{
    public class GetAllVehiclesQuery : IRequest<IEnumerable<Vehicle>>
    {
    }
}
