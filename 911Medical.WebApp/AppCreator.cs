using _911Medical.Application.Features.PatientFeatures.Commands;
using _911Medical.Application.Features.TripFeatures.Commands;
using _911Medical.Application.Features.VehicleFeatures.Commands;
using _911Medical.Application.Features.VehicleFeatures.Queries;
using _911Medical.Domain.Entities;
using _911Medical.WebApp.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace _911Medical.WebApp
{
    public class AppCreator : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AppCreator(IServiceProvider serviceProvider)
        { 
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            await context.Database.EnsureCreatedAsync();

            await CreateTestUsers(scope.ServiceProvider);
            await CreateVehicleStates(scope.ServiceProvider);
            //await CreateTestVehicles(scope.ServiceProvider);

            //await CreateTestPatients(scope.ServiceProvider);
            //await CreateTestTrips(scope.ServiceProvider);
            await CreateOpenIdClientApplication(scope.ServiceProvider);
        }

        private async Task CreateOpenIdClientApplication(IServiceProvider serviceProvider)
        {
            var manager = serviceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            var app = await manager.FindByClientIdAsync("911Medical_Api");

            if (app is not null)
            {
                await manager.DeleteAsync(app);
            }

            if (await manager.FindByClientIdAsync("911Medical_Api") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "911Medical_Api",
                    ClientSecret = "003163EA-BED9-4DA8-9F80-EDB285D3019A",
                    DisplayName = "My Medical Web application",
                    Permissions =
                    {
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Authorization,
                        Permissions.GrantTypes.ClientCredentials,
                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Roles,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Address,
                        Permissions.Prefixes.Scope + "api",
                        Permissions.ResponseTypes.Code
                    },

                });
            }
        }

        public async Task CreateTestUsers(IServiceProvider serviceProvider)
        {
            return;
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            foreach (var identityUser in userManager.Users)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(identityUser);

                var result = await userManager.ResetPasswordAsync(identityUser, token, "Test!123");
            }
        }

        private async Task CreateTestVehicles(IServiceProvider serviceProvider)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            for (int i = 1; i < 10; i++)
            {
                var cmd = new CreateVehicleCommand()
                {
                    RegNumber = $"MBZU{589 + 10 * 1}",
                    VehicleType = Domain.Entities.VehicleType.Van,
                    Description = $"Avto_{i}"
                };

                await mediator.Send(cmd);
            }
        }

        private async Task CreateVehicleStates(IServiceProvider serviceProvider)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var vehicles = await mediator.Send(new GetAllVehiclesQuery());

            var cities = new[] { "Ptuj", "Maribor", "Kranj", "Koper", "Hajdina" };

            foreach (var vehicle in vehicles)
            {
                var skip = (int)(DateTime.UtcNow.Ticks % cities.Count() - 1);

                var currentCity = cities.Skip(skip).FirstOrDefault();

                await mediator.Send(new UpdateVehicleStatusCommand() { 
                    VehicleId = vehicle.Id, 
                    Status = VehicleStatus.Connected,
                    CurrentCity = currentCity 
                });
            }
        }

        private async Task CreateTestPatients(IServiceProvider serviceProvider)
        {
            var firstNames = new[] { "Bojan", "Aleš", "Eva", "Ana", "Ivana" };
            var lastNames = new[] { "Novak", "Bedrač", "Potočnik", "Kac", "Pernat" };
            var addresses = new[] {
                 new Address("Mariborska cesta 33", null, "2250", "Ptuj", null, "SI"),
                 new Address("Ptujska cesta 112", null, "2000", "Maribor", null, "SI"),
                 new Address("Ljubljanska cesta 45", null, "4000", "Kranj", null, "SI"),
                 new Address("Ljubljanska cesta 2", null, "6000", "Koper", null, "SI"),
                 new Address("Zg. Hajdina 44a", null, "2288", "Hajdina", null, "SI"),
            };

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            for (int i = 0; i < 5; i++)
            {
                var cmd = new CreatePatientCommand()
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    HomeAddress = addresses[i],
                };

                await mediator.Send(cmd);
            }
        }

        private async Task CreateTestTrips(IServiceProvider serviceProvider)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            for (int i = 1; i < 5; i++)
            {
                var cmd = new EnqueueNewTripCommand()
                {
                    PatientId = i,
                };

                await mediator.Send(cmd);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

}
