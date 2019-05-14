using BusinessApi.Contracts.Capabilities.OnBoarding;
using Crm.System.Contract;

namespace Crm.NexusAdapter.Service.Capabilities.OnBoarding.Logic
{
    /// <inheritdoc />
    public class OnBoardingCapability : IOnBoardingCapability
    {
        /// <inheritdoc />
        public OnBoardingCapability(ICrmSystem system)
        {
            ApplicantService = new ApplicantLogic(system);
            MemberService = new MemberLogic(system);
        }

        /// <inheritdoc />
        public IApplicantService ApplicantService { get; }

        /// <inheritdoc />
        public IMemberService MemberService { get; }
    }
}
