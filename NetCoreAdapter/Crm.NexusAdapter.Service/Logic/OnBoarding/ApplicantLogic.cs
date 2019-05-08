﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Crm.System.Contract;
using Crm.System.Contract.Model;

namespace Crm.NexusAdapter.Service.Logic.OnBoarding
{
    /// <summary>
    /// Implements logic for of <see cref="IApplicantService"/>
    /// </summary>
    public class ApplicantLogic : IApplicantService
    {
        private readonly ICrmSystem _crmSystem;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicantLogic(ICrmSystem crmSystem)
        {
            _crmSystem = crmSystem;
        }

        /// <inheritdoc />
        public async Task<string> CreateAsync(Applicant item, CancellationToken token = new CancellationToken())
        {
            var lead = new Lead().From(item);
            var id = await _crmSystem.LeadFunctionality.CreateAsync(lead);
            return id.ToIdString();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Applicant>> ReadAllAsync(int limit = 2147483647, CancellationToken token = new CancellationToken())
        {
            var leads = await _crmSystem.LeadFunctionality.ReadAllAsync();
            var applicants = leads
                .Where(lead => lead.Status == Lead.StatusEnum.Active)
                .Select(lead => new Applicant().From(lead));
            return await Task.FromResult(applicants);
        }

        /// <inheritdoc />
        public async Task<string> ApproveAsync(string id, CancellationToken token = default(CancellationToken))
        {
            var memberId = await _crmSystem.LeadFunctionality.QualifyAsync(id.ToGuid());
            return memberId.ToIdString();
        }

        /// <inheritdoc />
        public async Task RejectAsync(string id, CancellationToken token = default(CancellationToken))
        {
            await _crmSystem.LeadFunctionality.RejectAsync(id.ToGuid(), "Application rejected.");
        }

        /// <inheritdoc />
        public async Task WithdrawAsync(string id, CancellationToken token = default(CancellationToken))
        {
            await _crmSystem.LeadFunctionality.RejectAsync(id.ToGuid(), "Application withdrawn.");
        }
    }
}