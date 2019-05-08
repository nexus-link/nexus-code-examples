using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Microsoft.AspNetCore.Mvc;

namespace Crm.NexusAdapter.Service.Adapter.Controllers
{
    /// <summary>
    /// Service implementation of <see cref="IMemberService"/>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase, IMemberService
    {
        private readonly IOnBoardingCapability _capability;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capability">The logic layer</param>
        public MembersController(IOnBoardingCapability capability)
        {
            _capability = capability;
        }

        /// <inheritdoc />
        [HttpGet]
        public async Task<IEnumerable<Member>> ReadAllAsync()
        {
            return await _capability.MemberService.ReadAllAsync();
        }
    }
}
