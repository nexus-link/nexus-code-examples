using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Controllers.Capabilities.OnBoarding;
using Microsoft.AspNetCore.Mvc;

namespace Crm.NexusAdapter.Service.OnBoarding.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : MembersControllerTemplate
    {
        /// <inheritdoc />
        public MembersController(IOnBoardingCapability capability)
        :base(capability)
        {
        }
    }
}
