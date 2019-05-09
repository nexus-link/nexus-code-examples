using Crm.NexusAdapter.Contract;
using Crm.System.Contract;

namespace Crm.System.Sdk
{
    public class CrmSystemMock : ICrmSystem
    {
        /// <inheritdoc />
        public CrmSystemMock(IAdapterService adapterService)
        {
            var contactService = new ContactFunctionality();
            ContactFunctionality = contactService;
            LeadFunctionality = new LeadFunctionality(contactService, adapterService);
        }

        /// <inheritdoc />
        public ILeadFunctionality LeadFunctionality { get; }

        /// <inheritdoc />
        public IContactFunctionality ContactFunctionality { get; }
    }
}
