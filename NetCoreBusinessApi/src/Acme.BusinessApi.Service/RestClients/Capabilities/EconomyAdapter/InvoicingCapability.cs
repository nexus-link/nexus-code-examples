using System.Net.Http;
using Microsoft.Rest;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing;

namespace Acme.BusinessApi.Service.RestClients.Capabilities.EconomyAdapter
{
    /// <inheritdoc />
    public class InvoicingCapability : IInvoicingCapability
    {
        /// <inheritdoc />
        public InvoicingCapability(string baseUrl, HttpClient httpClient, ServiceClientCredentials credentials)
        {
            InvoiceService = new InvoiceRestClient($"{baseUrl}/Invoices", httpClient, credentials);
        }
        /// <inheritdoc />
        public IInvoiceService InvoiceService { get; set; }
    }
}
