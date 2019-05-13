using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Controllers.Capabilities.OnBoarding;
using Microsoft.AspNetCore.Mvc;

namespace Crm.NexusAdapter.Service.Capabilities.OnBoarding.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ApplicantsControllerTemplate
    {
        /// <inheritdoc />
        public ApplicantsController(IOnBoardingCapability capability)
            : base(capability)
        {
        }
    }
}
