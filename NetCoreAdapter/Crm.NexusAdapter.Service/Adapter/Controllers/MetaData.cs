using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Libraries.Core.Error.Logic;

namespace Crm.NexusAdapter.Service.Adapter.Controllers
{
    /// <summary>
    /// Service implementation of <see cref="IApplicantService"/>
    /// </summary>
    public abstract class MetaDataController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public Task<string> CreateAsync(Applicant item, CancellationToken token = new CancellationToken())
        {
            throw new FulcrumNotImplementedException();
        }
    }
}
