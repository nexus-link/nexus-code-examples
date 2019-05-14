using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.System.Contract.Model;

namespace Crm.System.Contract
{
    /// <summary>
    /// The contract for the customer service
    /// </summary>
    public interface IContactFunctionality
    {
        /// <summary>
        /// Get a list of all customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contact>> ReadAllAsync();

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">The id of the customer to delete.</param>
        Task Delete(Guid id);

        /// <summary>
        /// Delete all contacts.
        /// </summary>
        /// <remarks>This method really removes all contacts. Use with caution!</remarks>
        Task DeleteAll();
    }
}