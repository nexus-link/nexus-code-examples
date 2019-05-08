using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Crm.System.Contract;

namespace Crm.NexusAdapter.Service.Adapter.Logic.OnBoarding
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
        public async Task<IEnumerable<Member>> ReadAllAsync()
        {
            var contacts = await _crmSystem.ContactFunctionality.ReadAllAsync();
            var members = contacts.Select(contact => new Member().From(contact));
            return await Task.FromResult(members);
        }
    }
}
