using _911Medical.Application.Features.VehicleFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OpenIddict.Server.AspNetCore;
using System;
using System.Threading.Tasks;

namespace _911Medical.WebApp.Hubs
{
    [Authorize(Policy = "SignalHub")]
    public class VehicleHub : Hub<IVehicleHubClient>
    {
        private readonly IMediator _mediator;

        public VehicleHub(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task VehicleConnect(int vehicleId)
        {
            Context.Items.Add("VehicleId", vehicleId);

            var cmd = new UpdateVehicleStatusCommand()
            { 
                VehicleId = vehicleId,
                User = Context.User?.Identity?.Name,
                Status = Domain.Entities.VehicleStatus.Connected 
            };

            await _mediator.Send(cmd);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            int vehicleId = (int)Context.Items["VehicleId"];

            var cmd = new UpdateVehicleStatusCommand()
            {
                VehicleId = vehicleId,
                User = Context.User?.Identity?.Name,
                Status = Domain.Entities.VehicleStatus.Unknown
            };

            await _mediator.Send(cmd);
        }
    }

    public interface IVehicleHubClient
    {
        void TripStarted(int tripId);

        void TripComplete(int tripId);

    }
}
