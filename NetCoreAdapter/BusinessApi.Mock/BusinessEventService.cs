using System;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.Integration;
using BusinessApi.Contracts.Events;
using Newtonsoft.Json;

namespace BusinessApi.Mock
{
    /// <inheritdoc />
    public class BusinessEventService : IBusinessEventService
    {
        /// <inheritdoc />
        public Task PublishAsync(IPublishableEvent @event)
        {
            var eventBody = JsonConvert.SerializeObject(@event, Formatting.Indented);
            Console.WriteLine($"POST {@event.Metadata.EntityName}/{@event.Metadata.EventName}/{@event.Metadata.MajorVersion}/{@event.Metadata.MinorVersion}");
            Console.WriteLine(eventBody);
            return Task.CompletedTask;
        }
    }
}