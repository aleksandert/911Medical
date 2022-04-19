using _911Medical.Application.Features.TripFeatures.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace _911Medical.WebApp.Hubs
{
    public class EventsClientDispatcher : INotificationHandler<TripDispatchedEvent>
    {
        private readonly IHubContext<VehicleHub> _hubContext;

        public EventsClientDispatcher(IHubContext<VehicleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(TripDispatchedEvent notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.User("").SendAsync(nameof(TripDispatchedEvent), notification, cancellationToken);
        }
    }
}
