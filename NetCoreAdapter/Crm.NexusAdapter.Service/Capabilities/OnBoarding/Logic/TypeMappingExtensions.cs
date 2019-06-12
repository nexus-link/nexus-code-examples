using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Crm.System.Contract.Model;

namespace Crm.NexusAdapter.Service.Capabilities.OnBoarding.Logic
{
    /// <summary>
    /// Extensions for mapping between types
    /// </summary>
    internal static class TypeMappingExtensions
    {
        /// <summary>
        /// Map from <see cref="Lead"/> to <see cref="Applicant"/>.
        /// </summary>
        public static Applicant From(this Applicant target, Lead source)
        {
            target.Name = source.Name;
            target.Id = source.Id.ToIdString();
            return target;
        }

        /// <summary>
        /// Map from <see cref="Contact"/> to <see cref="Lead"/>.
        /// </summary>
        public static Member From(this Member target, Contact source)
        {
            target.Name = source.Name;
            target.MembershipNumber = source.CustomerNumber;
            target.Id = source.Id.ToIdString();
            return target;
        }

        /// <summary>
        /// Map from <see cref="Applicant"/> to <see cref="Lead"/>.
        /// </summary>
        public static Lead From(this Lead target, Applicant source)
        {
            target.Name = source.Name;
            return target;
        }
    }
}
