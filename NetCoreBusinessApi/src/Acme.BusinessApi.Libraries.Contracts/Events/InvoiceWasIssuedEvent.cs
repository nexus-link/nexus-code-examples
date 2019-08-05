using System;
using Nexus.Link.Services.Contracts.Capabilities.Integration.BusinessEvents.Model;

namespace Acme.BusinessApi.Libraries.Contracts.Events
{
    /// <summary>
    /// This event is published whenever a new Invoices has been published.
    /// </summary>
    public class InvoiceWasIssuedEvent : IPublishableEvent
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
        /// When the invoice was created, i.e. the time when it can be accounted for.
        /// </summary>
        public DateTime IssuedAtUtcTime { get; set; }
    }
}
