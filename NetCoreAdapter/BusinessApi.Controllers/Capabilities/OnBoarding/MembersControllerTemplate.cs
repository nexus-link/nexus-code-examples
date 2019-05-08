using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApi.Controllers.Capabilities.OnBoarding
{
    /// <summary>
    /// Service implementation of <see cref="IMemberService"/>
    /// </summary>
    public abstract class MembersControllerTemplate : ControllerBase, IMemberService
    {
        protected readonly IOnBoardingCapability Capability;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capability">The logic layer</param>
        protected MembersControllerTemplate(IOnBoardingCapability capability)
        {
            Capability = capability;
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Member>> ReadAllAsync(int limit = 2147483647, CancellationToken token = new CancellationToken())
        {
            return await Capability.MemberService.ReadAllAsync(limit, token);
        }
    }
}
