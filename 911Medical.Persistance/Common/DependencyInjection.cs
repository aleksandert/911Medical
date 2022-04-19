using _911Medical.Domain.Interfaces;
using _911Medical.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace _911Medical.Persistance.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MedicalDbContext>(options => 
                //options.UseInMemoryDatabase(databaseName: "911Medical")
                options.UseSqlServer(connectionString)
            );

            //services.AddScoped(typeof(EFRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            return services;
        }
    }
}
