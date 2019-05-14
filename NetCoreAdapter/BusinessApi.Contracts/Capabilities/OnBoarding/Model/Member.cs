using Nexus.Link.Libraries.Core.Assert;

namespace BusinessApi.Contracts.Capabilities.OnBoarding.Model
{
    /// <summary>
    /// Information about a member
    /// </summary>
    public class Member : IValidatable
    {
        /// <summary>
        /// The internal id of the member.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unique number to be used in all communication
        /// </summary>
        public string MembershipNumber { get; set; }

        /// <summary>
        /// The name of the member
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            // Validate the fields of this object
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, $"{nameof(Name)}", errorLocation);
            // Validate the fields of this object
            FulcrumValidate.IsNotNullOrWhiteSpace(MembershipNumber, $"{nameof(MembershipNumber)}", errorLocation);
        }
    }
}
