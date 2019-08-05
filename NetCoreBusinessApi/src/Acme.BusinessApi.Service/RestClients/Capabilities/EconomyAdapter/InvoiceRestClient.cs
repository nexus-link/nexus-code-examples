using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.Web.RestClient;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing.Model;

namespace Acme.BusinessApi.Service.RestClients.Capabilities.EconomyAdapter
{
    /// <inheritdoc cref="IInvoiceService" />
    public class InvoiceRestClient : CrudRestClient<Invoice, string>, IInvoiceService
    {
        /// <inheritdoc />
        public InvoiceRestClient(string baseUri, HttpClient httpClient, ServiceClientCredentials credentials) : base(baseUri, httpClient, credentials)
        {
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<Invoice>> ReadAllLateWithPagingAsync(bool descending = false, int offset = 0, int? limit = null,
            CancellationToken token = default(CancellationToken))
        {
            return await GetAsync<PageEnvelope<Invoice>>("Late", null, token);
        }
    }
}