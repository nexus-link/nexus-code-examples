using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Services.Contracts.Capabilities.Integration.BusinessEvents;
using Nexus.Link.Services.Controllers.Capabilities.Integration.BusinessEvents;

namespace Acme.BusinessApi.Service.Controllers.Capabilities.Integration
{
    /// <inheritdoc cref="BusinessEventsControllerBase" />
    [Route("api/Integration/v1/BusinessEvents/[controller]")]
    [ApiController]
    public class BusinessEventsController : BusinessEventsControllerBase
    {
        /// <inheritdoc />
        public BusinessEventsController(IBusinessEventsCapability capability) : base(capability)
        {
        }
    }
}