using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.Assert;

namespace BusinessApi.Controllers.Capabilities.OnBoarding
{
    /// <summary>
    /// Service implementation of <see cref="IApplicantService"/>
    /// </summary>
    public abstract class ApplicantsControllerTemplate : ControllerBase, IApplicantService
    {
        protected readonly IOnBoardingCapability Capability;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capability">The logic layer</param>
        protected ApplicantsControllerTemplate(IOnBoardingCapability capability)
        {
            Capability = capability;
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("")]
        public async Task<string> CreateAsync(Applicant item, CancellationToken token = new CancellationToken())
        {
            ServiceContract.Require(item.Id == null, $"The {nameof(item.Id)} field must be null.");
            return await Capability.ApplicantService.CreateAsync(item, token);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Applicant>> ReadAllAsync(int limit, CancellationToken token = new CancellationToken())
        {
            return await Capability.ApplicantService.ReadAllAsync(limit, token);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Approve")]
        public async Task<string> ApproveAsync(string id, CancellationToken token = new CancellationToken())
        {
            ServiceContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            return await Capability.ApplicantService.ApproveAsync(id, token);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Reject")]
        public async Task RejectAsync(string id, CancellationToken token = new CancellationToken())
        {
            ServiceContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            await Capability.ApplicantService.RejectAsync(id, token);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Withdraw")]
        public async Task WithdrawAsync(string id, CancellationToken token = new CancellationToken())
        {
            ServiceContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            await Capability.ApplicantService.WithdrawAsync(id, token);
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("")]
        public async Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            ServiceContract.Require(!FulcrumApplication.IsInProductionOrProductionSimulation, "This method can\'t be called in production.");
            await Capability.ApplicantService.DeleteAllAsync(token);
        }
    }
}
