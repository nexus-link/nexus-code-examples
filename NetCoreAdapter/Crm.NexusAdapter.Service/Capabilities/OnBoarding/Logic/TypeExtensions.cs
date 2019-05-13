using BusinessApi.Contracts.Capabilities.OnBoarding.Model;
using Crm.NexusAdapter.Service.Logic;
using Crm.System.Contract.Model;

namespace Crm.NexusAdapter.Service.Capabilities.OnBoarding.Logic
{
    public static class TypeExtensions
    {
        public static Applicant From(this Applicant target, Lead source)
        {
            target.Name = source.Name;
            target.Id = source.Id.ToIdString();
            return target;
        }
        public static Member From(this Member target, Contact source)
        {
            target.Name = source.Name;
            target.MembershipNumber = source.CustomerNumber;
            target.Id = source.Id.ToIdString();
            return target;
        }

        public static Lead From(this Lead target, Applicant source)
        {
            target.Name = source.Name;
            return target;
        }
    }
}
