using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Error.Logic;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.AspNet.ControllerHelpers;
using Nexus.Link.Libraries.Crud.Interfaces;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing.Model;

namespace Acme.BusinessApi.Libraries.Controllers.Capabilities.Economy
{
    /// <summary>
    /// Service implementation of <see cref="IInvoiceService"/>
    /// </summary>
    [Route("api/Economy/v1/[controller]")]
    [ApiController]
    public abstract class InvoicesControllerBase : IInvoiceService
    {
        /// <summary>
        /// The capability for this controller
        /// </summary>
        protected readonly IInvoicingCapability Capability;

        /// <summary>
        /// The CrudController for this controller
        /// </summary>
        protected readonly ICrud<Invoice, string> CrudController;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capability">The logic layer</param>
        protected InvoicesControllerBase(IInvoicingCapability capability)
        {
            Capability = capability;
            CrudController = new CrudControllerHelper<Invoice>(capability.InvoiceService);
        }

        /// <inheritdoc />
        [HttpGet("Late")]
        [Authorize(Roles = "business-api-caller")]
        public virtual async Task<PageEnvelope<Invoice>> ReadAllLateWithPagingAsync(bool descending = false, int offset = 0, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireGreaterThanOrEqualTo(0, offset, nameof(offset));
            if (limit.HasValue) ServiceContract.RequireGreaterThanOrEqualTo(0, limit.Value, nameof(limit));
            return await Capability.InvoiceService.ReadAllLateWithPagingAsync(descending, offset, limit, token);
        }

        /// <inheritdoc />
        [HttpGet("")]
        [Authorize(Roles = "business-api-caller")]
        public async Task<PageEnvelope<Invoice>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireGreaterThanOrEqualTo(0, offset, nameof(offset));
            if (limit.HasValue) ServiceContract.RequireGreaterThanOrEqualTo(0, limit.Value, nameof(limit));
            return await Capability.InvoiceService.ReadAllWithPagingAsync(offset, limit, token);
        }

        /// <inheritdoc />
        [HttpGet("{id}")]
        [Authorize(Roles = "business-api-caller")]
        public virtual Task<Invoice> ReadAsync(string id, CancellationToken token = default(CancellationToken))
        {
            return CrudController.ReadAsync(id, token);
        }

        /// <inheritdoc />
        [HttpPost("{id}")]
        [Authorize(Roles = "business-api-caller")]
        public virtual async Task CreateWithSpecifiedIdAsync(string id, Invoice item, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhiteSpace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            await CrudController.CreateWithSpecifiedIdAsync(id, item, token);
        }

        /// <inheritdoc />
        Task<Invoice> ICreateWithSpecifiedId<Invoice, Invoice, string>.CreateWithSpecifiedIdAndReturnAsync(string id, Invoice item, CancellationToken token)
        {
            throw new FulcrumNotImplementedException("This method is intentionally not implemented.");
        }
    }
}
