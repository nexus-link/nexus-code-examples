namespace BusinessApi.Contracts.Capabilities.OnBoarding
{
    /// <summary>
    /// The CRM capability
    /// </summary>
    public interface IOnBoardingCapability
    {
        IApplicantService ApplicantService { get; }
        IMemberService MemberService { get; }
    }
}
