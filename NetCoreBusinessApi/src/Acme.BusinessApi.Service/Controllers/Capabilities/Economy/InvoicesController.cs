using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing;
using Acme.BusinessApi.Libraries.Controllers.Capabilities.Economy;

namespace Acme.BusinessApi.Service.Controllers.Capabilities.Economy
{
    /// <inheritdoc cref="InvoicesControllerBase" />
    public class InvoicesController : InvoicesControllerBase
    {
        /// <inheritdoc />
        public InvoicesController(IInvoicingCapability capability) : base(capability)
        {
        }
    }
}