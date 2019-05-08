﻿using BusinessApi.Contracts.Capabilities.OnBoarding;
using Crm.System.Contract;

namespace Crm.NexusAdapter.Service.Logic.OnBoarding
{
    public class OnBoardingLogic : IOnBoardingCapability
    {
        public OnBoardingLogic(ICrmSystem system)
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