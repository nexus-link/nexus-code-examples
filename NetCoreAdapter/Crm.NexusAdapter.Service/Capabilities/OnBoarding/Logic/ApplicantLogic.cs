using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Crm.NexusAdapter.Service.Logic;
using Crm.System.Contract;
using Crm.System.Contract.Exceptions;
using Crm.System.Contract.Model;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Error.Logic;

namespace Crm.NexusAdapter.Service.Capabilities.OnBoarding.Logic
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
            try
            {
                var memberId = await _crmSystem.LeadFunctionality.QualifyAsync(id.ToGuid());
                return memberId.ToIdString();
            }
            catch (NotFoundException)
            {
                throw new FulcrumNotFoundException($"The application with id {id} could not be found.");
            }
            catch (Exception e)
            {
                throw e.ToNexusException();
            }
        }

        /// <inheritdoc />
        public async Task RejectAsync(string id, CancellationToken token = default(CancellationToken))
        {
            try
            {
                await _crmSystem.LeadFunctionality.RejectAsync(id.ToGuid(), "Application rejected.");
            }
            catch (NotFoundException)
            {
                throw new FulcrumNotFoundException($"The application with id {id} could not be found.");
            }
            catch (Exception e)
            {
                throw e.ToNexusException();
            }
        }

        /// <inheritdoc />
        public async Task WithdrawAsync(string id, CancellationToken token = default(CancellationToken))
        {
            try
            {
                await _crmSystem.LeadFunctionality.RejectAsync(id.ToGuid(), "Application withdrawn.");
            }
            catch (NotFoundException)
            {
                throw new FulcrumNotFoundException($"The application with id {id} could not be found.");
            }
            catch (Exception e)
            {
                throw e.ToNexusException();
            }
        }

        /// <inheritdoc />
        public async Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            InternalContract.Require(!FulcrumApplication.IsInProductionOrProductionSimulation, "This method can\'t be called in production.");
            await _crmSystem.LeadFunctionality.DeleteAll();
        }
    }
}
