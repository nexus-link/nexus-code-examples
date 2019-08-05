using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Services.Contracts.Capabilities.Integration.Authentication;
using Nexus.Link.Services.Controllers.Capabilities.Integration.Authentication;

namespace Acme.BusinessApi.Service.Controllers.Capabilities.Integration
{
    /// <inheritdoc cref="TokensControllerBase" />
    [Route("api/Integration/v1/Authentication/[controller]")]
    [ApiController]
    public class PublicKeysController : PublicKeysControllerBase
    {
        /// <inheritdoc />
        public PublicKeysController(IAuthenticationCapability capability) : base(capability)
        {
        }
    }
}
