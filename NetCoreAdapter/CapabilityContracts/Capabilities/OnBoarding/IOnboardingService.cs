namespace CapabilityContracts.OnBoarding
{
    /// <summary>
    /// The CRM capability
    /// </summary>
    public interface IOnBoardingService
    {
        IApplicantService ApplicantService { get; }
        IMemberService MemberService { get; }
    }
}
