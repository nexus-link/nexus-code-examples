using System;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.Integration;
using BusinessApi.Contracts.Events;
using Crm.NexusAdapter.Contract;

namespace Crm.NexusAdapter.Service.Adapter.Logic
{
    public class AdapterServiceForSystem : IAdapterService
    {
        private readonly INexusApi _nexusApi;

        public AdapterServiceForSystem(INexusApi nexusApi)
        {
            _nexusApi = nexusApi;
        }

        /// <inheritdoc />
        public async Task LeadWasQualified(Guid leadId, Guid contactId, DateTimeOffset approvedAt)
        {
            var @event = new MemberApprovedEvent
            {
                MemberId = contactId.ToIdString(),
                ApprovedAt = approvedAt.ToIso8061Time()
            };
            await _nexusApi.BusinessEventService.PublishAsync(@event);
        }
    }
}
