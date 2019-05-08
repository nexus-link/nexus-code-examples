using System.Threading.Tasks;
using CapabilityContracts.Events;

namespace CapabilityContracts.Integration
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
