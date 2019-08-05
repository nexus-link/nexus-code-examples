using System;
using Nexus.Link.Services.Contracts.Capabilities.Integration.BusinessEvents.Model;

namespace Acme.BusinessApi.Libraries.Contracts.Events
{
    /// <summary>
    /// This event is published whenever an Invoices is overdue.
    /// </summary>
    public class InvoiceBecameOverdueEvent : IPublishableEvent
    {
        /// <inheritdoc />
        public EventMetadata Metadata { get; set; } = new EventMetadata("Invoice", "Paid", 1, 0);

        /// <summary>
        /// The id of the invoice
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// The total amount for this invoice.
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// When the invoice expires, i.e. the latest time that it has to be paid
        /// </summary>
        public DateTime ExpiresAtUtcTime { get; set; }
    }
}
