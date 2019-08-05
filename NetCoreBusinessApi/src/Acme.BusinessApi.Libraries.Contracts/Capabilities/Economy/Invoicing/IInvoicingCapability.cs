using Nexus.Link.Libraries.Crud.Interfaces;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing.Model;

namespace Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing
{
    /// <summary>
    /// The services
    /// </summary>
    public interface IInvoicingCapability : ICrudable<Invoice, string>
    {
        /// <summary>
        /// The service for <see cref="Invoice"/>.
        /// </summary>
        IInvoiceService InvoiceService { get; set; }
    }
}
