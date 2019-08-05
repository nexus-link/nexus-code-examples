using System;
using Nexus.Link.Services.Contracts.Capabilities.Integration.BusinessEvents.Model;

namespace Acme.BusinessApi.Libraries.Contracts.Events
{
    /// <summary>
    /// This event is published whenever an Invoice has been paid in full.
    /// </summary>
    public class InvoiceWasPaidInFullEvent : IPublishableEvent
    {
        /// <inheritdoc />
        public EventMetadata Metadata { get; set; } = new EventMetadata("Invoice", "Paid", 1, 0);

        /// <summary>
        /// The id of the invoice
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// The total amount for this invoice. The amount paid could theoretically have been more.
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// When the invoice expires, i.e. the latest time that it has to be paid
        /// </summary>
        public DateTime ExpiresAtUtcTime { get; set; }

        /// <summary>
        /// The actual time when the invoice was paid, or at least when our organization noted it as paid
        /// </summary>
        public DateTime PaidAtUtcTime { get; set; }
    }
}
