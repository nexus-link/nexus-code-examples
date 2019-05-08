using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Nexus.Link.Libraries.Crud.Interfaces;

namespace BusinessApi.Contracts.Capabilities.OnBoarding
{
    /// <summary>
    /// Methods for dealing with an applicant.
    /// </summary>
    public interface IApplicantService : ICreate<Applicant, string>, IReadAll<Applicant, string>
    {
        /// <summary>
        /// Approve an applicant to become a member
        /// </summary>
        /// <param name="id">The id of the applicant to approve.</param>
        /// <param name="token"></param>
        /// <returns>The internal id of the new member record.</returns>
        Task<string> ApproveAsync(string id, CancellationToken token = new CancellationToken());

        /// <summary>
        /// Deny an applicant to become member
        /// </summary>
        /// <param name="id">The id of the lead to qualify.</param>
        /// <param name="token"></param>
        Task RejectAsync(string id, CancellationToken token = new CancellationToken());

        /// <summary>
        /// The applicant has withdrawn the application
        /// </summary>
        /// <param name="id">The id of the applicant.</param>
        /// <param name="token"></param>
        Task WithdrawAsync(string id, CancellationToken token = new CancellationToken());
    }
}