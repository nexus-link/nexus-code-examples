using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessApi.Contracts.Capabilities.OnBoarding.Model;

namespace BusinessApi.Contracts.Capabilities.OnBoarding
{
    /// <summary>
    /// Methods for dealing with a member
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// Get a list of all members
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Member>> ReadAllAsync();
    }
}