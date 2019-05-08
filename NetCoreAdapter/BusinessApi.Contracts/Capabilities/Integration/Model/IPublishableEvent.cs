namespace BusinessApi.Contracts.Capabilities.Integration.Model
{
    /// <summary>
    /// Metadata that are needed for publishing events.
    /// </summary>
    public interface IPublishableEvent
    {
        EventMetadata Metadata { get; }
    }
}