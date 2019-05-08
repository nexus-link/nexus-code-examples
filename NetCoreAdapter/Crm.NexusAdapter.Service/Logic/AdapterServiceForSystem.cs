using System;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.Integration;
using BusinessApi.Contracts.Events;
using Crm.NexusAdapter.Contract;

namespace Crm.NexusAdapter.Service.Logic
{
    public class AdapterServiceForSystem : IAdapterService
    {
        private readonly IIntegrationCapability _integrationCapability;

        public AdapterServiceForSystem(IIntegrationCapability integrationCapability)
        {
            _integrationCapability = integrationCapability;
        }

        /// <inheritdoc />
        public async Task LeadWasQualified(Guid leadId, Guid contactId, DateTimeOffset approvedAt)
        {
            var @event = new MemberApprovedEvent
            {
                MemberId = contactId.ToIdString(),
                ApprovedAt = approvedAt.ToIso8061Time()
            };
            await _integrationCapability.BusinessEventService.PublishAsync(@event);
        }
    }
}
