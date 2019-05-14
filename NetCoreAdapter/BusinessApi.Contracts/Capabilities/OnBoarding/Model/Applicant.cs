using Nexus.Link.Libraries.Core.Assert;

namespace BusinessApi.Contracts.Capabilities.OnBoarding.Model
{
    /// <summary>
    /// Information about an applicant
    /// </summary>
    public class Applicant : IValidatable
    {
        /// <summary>
        /// The id of the object
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the applicant
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            // Validate the fields of this object
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, $"{nameof(Name)}", errorLocation);
        }
    }
}
