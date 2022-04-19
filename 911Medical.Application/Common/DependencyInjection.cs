using _911Medical.Application.Common.Behaviors;
using _911Medical.Application.Services.TripServices;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace _911Medical.Application.Common
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers required services for application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // register mediatr commands & queries and associated handlers from current assembly
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // register Mediatr validation behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // register fluent validators found in this assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            // register specific domain service
            services.AddSingleton<FindClosestVehicleService>();

            return services;
        }
    }
}
