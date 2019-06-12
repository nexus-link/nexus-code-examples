using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Crm.System.Contract;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.Assert;

namespace Crm.NexusAdapter.Service.Capabilities.OnBoarding.Logic
{
    /// <summary>
    /// Implements logic for of <see cref="IMemberService"/>
    /// </summary>
    public class MemberLogic : IMemberService
    {
        private readonly ICrmSystem _crmSystem;

        /// <summary>
        /// Constructor
        /// </summary>
        public MemberLogic(ICrmSystem crmSystem)
        {
            _crmSystem = crmSystem;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Member>> ReadAllAsync(int limit = 2147483647, CancellationToken token = new CancellationToken())
        {
            var contacts = await _crmSystem.ContactFunctionality.ReadAllAsync();
            var members = contacts.Select(contact => new Member().From(contact));
            return await Task.FromResult(members);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            await _crmSystem.ContactFunctionality.Delete(id.ToGuid());
        }

        /// <inheritdoc />
        public async Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            InternalContract.Require(!FulcrumApplication.IsInProductionOrProductionSimulation, "This method can\'t be called in production.");
            await _crmSystem.ContactFunctionality.DeleteAll();
        }
    }
}
