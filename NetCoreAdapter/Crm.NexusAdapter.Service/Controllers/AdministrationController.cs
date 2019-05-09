using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Error.Logic;

namespace Crm.NexusAdapter.Service.Controllers
{
    /// <summary>
    /// Administrative services.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IOnBoardingCapability _capability;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capability"></param>
        public AdministrationController(IOnBoardingCapability capability)
        {
            _capability = capability;
        }

        /// <summary>
        /// Remove all members and applicants.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Reset")]
        public async Task ResetAsync(CancellationToken token = new CancellationToken())
        {
            ServiceContract.Require(!FulcrumApplication.IsInProductionOrProductionSimulation, $"This method can't be called in production.");
            var members = await _capability.MemberService.ReadAllAsync(int.MaxValue, token);
            var taskList = new List<Task>();
            foreach (var member in members)
            {
                var task = _capability.MemberService.DeleteAsync(member.Id, token);
                taskList.Add(task);
            }

            var t = _capability.ApplicantService.DeleteAllAsync(token);
            taskList.Add(t);
            await Task.WhenAll(taskList);
        }

        /// <summary>
        /// Add some members and applicants.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Seed")]
        public async Task SeedAsync(CancellationToken token = new CancellationToken())
        {
            ServiceContract.Require(!FulcrumApplication.IsInProductionOrProductionSimulation, $"This method can't be called in production.");
            var taskList = new List<Task>();

            // Add some applicants
            var task = _capability.ApplicantService.CreateAsync(new Applicant { Name = "Johnny B. Good" }, token);
            taskList.Add(task);
            task = _capability.ApplicantService.CreateAsync(new Applicant { Name = "Bad Cousin" }, token);
            taskList.Add(task);
            task = _capability.ApplicantService.CreateAsync(new Applicant { Name = "Willy Nilly" }, token);
            taskList.Add(task);
            await Task.WhenAll(taskList);

            // Add some approved members
            var id = await _capability.ApplicantService.CreateAsync(new Applicant { Name = "Donald Duck" }, token);
            await _capability.ApplicantService.ApproveAsync(id, token);
            id = await _capability.ApplicantService.CreateAsync(new Applicant { Name = "Mickey Mouse" }, token);
            await _capability.ApplicantService.ApproveAsync(id, token);
        }
    }
}
