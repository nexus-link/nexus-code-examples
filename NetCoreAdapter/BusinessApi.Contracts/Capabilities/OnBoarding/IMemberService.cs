using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Nexus.Link.Libraries.Crud.Interfaces;

namespace BusinessApi.Contracts.Capabilities.OnBoarding
{
    /// <summary>
    /// Methods for dealing with a member
    /// </summary>
    public interface IMemberService : IReadAll<Member, string>, IDelete<string>, IDeleteAll
    {
    }
}