using _911Medical.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Services.TripServices
{
    public class FindClosestVehicleService
    {
        private static readonly IDictionary<string, int> _distances = (new[]
        {
            new {From="Ptuj", To="Maribor", Distance = 30 },
            new {From="Ptuj", To="Kranj", Distance = 167 },
            new {From="Ptuj", To="Koper", Distance = 245 },
            new {From="Ptuj", To="Hajdina", Distance = 5 },
            new {From="Maribor", To="Ptuj", Distance = 30 },
            new {From="Maribor", To="Kranj", Distance = 155 },
            new {From="Maribor", To="Koper", Distance = 232 },
            new {From="Maribor", To="Hajdina", Distance = 25 },
            new {From="Kranj", To="Ptuj", Distance = 167 },
            new {From="Kranj", To="Maribor", Distance = 155 },
            new {From="Kranj", To="Koper", Distance = 131 },
            new {From="Kranj", To="Hajdina", Distance = 154 },
            new {From="Koper", To="Ptuj", Distance = 245 },
            new {From="Koper", To="Maribor", Distance = 232 },
            new {From="Koper", To="Kranj", Distance = 131 },
            new {From="Koper", To="Hajdina", Distance = 241 },
            new {From="Hajdina", To="Ptuj", Distance = 5 },
            new {From="Hajdina", To="Maribor", Distance = 25 },
            new {From="Hajdina", To="Kranj", Distance = 154 },
            new {From="Hajdina", To="Koper", Distance = 241 },
        }).ToDictionary(k => $"{k.From}-{k.To}", v => v.Distance);

        public FindClosestVehicleService()
        {
            
        }

        public Vehicle Find(IEnumerable<Vehicle> vehicles, Address address)
        {
            // get closest vehicle
            var skip = (int)(DateTime.UtcNow.Ticks % vehicles.Count() - 1);
            var vehicle = vehicles.Skip(skip).FirstOrDefault();

            return vehicle;
        }
    }
}
