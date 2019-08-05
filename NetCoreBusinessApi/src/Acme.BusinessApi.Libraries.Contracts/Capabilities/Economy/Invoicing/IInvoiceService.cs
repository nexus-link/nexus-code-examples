using System.Threading;
using System.Threading.Tasks;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.Interfaces;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing.Model;

namespace Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing
{
    /// <summary>
    /// Service for invoices
    /// </summary>
    public interface IInvoiceService : IRead<Invoice, string>, IReadAllWithPaging<Invoice, string>, ICreateWithSpecifiedId<Invoice, string>
    {
        /// <summary>
        /// Read all invoices that have passed their date of 
        /// </summary>
        /// <param name="descending">True means that the result is sorted with the oldest late invoice first,
        /// false with the newest late invoice first.</param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>A paged list with the late invoices.</returns>
        Task<PageEnvelope<Invoice>> ReadAllLateWithPagingAsync(bool descending = false, int offset = 0, int? limit = null, CancellationToken token = default(CancellationToken));
    }
}
