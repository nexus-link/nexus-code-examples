namespace BusinessApi.Contracts.Capabilities.Integration
{
    /// <summary>
    /// The services that the nexus API provides
    /// </summary>
    public interface IIntegrationCapability
    {
        /// <summary>
        /// Service for business events
        /// </summary>
        IBusinessEventService BusinessEventService { get; }
    }
}
