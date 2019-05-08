using System;
using System.Threading.Tasks;

namespace Crm.NexusAdapter.Contract
{
    public interface IAdapterService
    {
        Task LeadWasQualified(Guid leadId, Guid contactId, DateTimeOffset approvedAt);
    }
}