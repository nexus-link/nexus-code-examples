using System;
using Nexus.Link.Libraries.Core.Storage.Model;

namespace Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing.Model
{
    /// <summary>
    /// Data type for the entity Invoice
    /// </summary>
    public class Invoice : IUniquelyIdentifiable<string>
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <summary>
        /// The total amount for this invoice
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// When the invoice was created, i.e. the time when it can be accounted for.
        /// </summary>
        public DateTime IssuedAtUtcTime { get; set; }

        /// <summary>
        /// When the invoice expires, i.e. the latest time that it has to be paid
        /// </summary>
        public DateTime ExpiresAtUtcTime { get; set; }

        /// <summary>
        /// The actual time when the invoice was paid, or at least when our organization noted it as paid
        /// </summary>
        public DateTime? PaidAtUtcTime { get; set; }
    }
}