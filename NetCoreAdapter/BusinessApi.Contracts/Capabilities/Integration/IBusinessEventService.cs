using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.Integration.Model;
using BusinessApi.Contracts.Events;

namespace BusinessApi.Contracts.Capabilities.Integration
{
    /// <summary>
    /// Services for BusinessEvents
    /// </summary>
    public interface IBusinessEventService
    {
        /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="event">The event to publish.</param>
        Task PublishAsync(IPublishableEvent @event);
    }
}
