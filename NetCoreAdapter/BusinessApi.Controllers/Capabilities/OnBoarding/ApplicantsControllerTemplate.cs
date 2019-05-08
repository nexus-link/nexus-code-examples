using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Applicant>> ReadAllAsync()
        {
            return await Capability.ApplicantService.ReadAllAsync();
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("")]
        public async Task<string> CreateAsync(Applicant applicant)
        {
            ServiceContract.Require(applicant.Id == null, $"The {nameof(applicant.Id)} field must be null.");
            return await Capability.ApplicantService.CreateAsync(applicant);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Approve")]
        public async Task<string> ApproveAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            return await Capability.ApplicantService.ApproveAsync(id);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Reject")]
        public async Task RejectAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            await Capability.ApplicantService.RejectAsync(id);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Withdraw")]
        public async Task WithdrawAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            await Capability.ApplicantService.WithdrawAsync(id);
        }
    }
}
