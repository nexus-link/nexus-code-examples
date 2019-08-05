using Nexus.Link.Libraries.Core.Platform.Authentication;

namespace Acme.BusinessApi.Libraries.Sdk.Capabilities.Integration
{
    /// <inheritdoc />
    public class IntegrationCapability : Nexus.Link.Services.Implementations.Adapter.Capabilities.Integration.IntegrationCapability
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl">The URL to the integration capability</param>
        /// <param name="basicCredentials">ClientId and ClientSecret.</param>
        public IntegrationCapability(string baseUrl, AuthenticationCredentials basicCredentials) : base(baseUrl, basicCredentials)
        {
        }
    }
}
