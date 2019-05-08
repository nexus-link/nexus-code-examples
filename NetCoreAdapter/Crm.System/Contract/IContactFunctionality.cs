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
    }
}