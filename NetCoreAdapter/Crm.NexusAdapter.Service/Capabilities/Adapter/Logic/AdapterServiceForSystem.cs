﻿using System;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.Integration;
using BusinessApi.Contracts.Events;
using Crm.NexusAdapter.Contract;
using Crm.NexusAdapter.Service.Capabilities.OnBoarding.Logic;

namespace Crm.NexusAdapter.Service.Capabilities.Adapter.Logic
{
    /// <inheritdoc />
    public class AdapterServiceForSystem : IAdapterService
    {
        private readonly IIntegrationCapability _integrationCapability;

        /// <inheritdoc />
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
