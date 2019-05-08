using BusinessApi.Contracts.Capabilities.Integration;

namespace BusinessApi.Mock
{
    /// <inheritdoc />
    public class NexusApiMock : INexusApi
    {
        /// <inheritdoc />
        public NexusApiMock()
        {
            BusinessEventService = new BusinessEventService();
        }

        /// <inheritdoc />
        public IBusinessEventService BusinessEventService { get; }
    }
}