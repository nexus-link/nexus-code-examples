using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Services.Contracts.Capabilities.Integration.Authentication;
using Nexus.Link.Services.Controllers.Capabilities.Integration.Authentication;

namespace Acme.BusinessApi.Service.Controllers.Capabilities.Integration
{
    /// <inheritdoc cref="TokensControllerBase" />
    [Route("api/Integration/v1/Authentication/[controller]")]
    [ApiController]
    public class TokensController : TokensControllerBase
    {
        /// <inheritdoc />
        public TokensController(IAuthenticationCapability capability) : base(capability)
        {
        }
    }
}
