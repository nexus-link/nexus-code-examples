using System;
using Crm.System.Contract.Exceptions;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Error.Logic;

namespace Crm.NexusAdapter.Service.Capabilities.OnBoarding.Logic
{
    internal static class CommonTypeMappingExtensions
    {
        public static string ToIdString(this Guid source)
        {
            return source.ToString().ToLowerInvariant();
        }

        public static Guid ToGuid(this string source)
        {
            InternalContract.Require(Guid.TryParse(source, out var target), nameof(source));
            return target;
        }

        public static string ToIso8061Time(this DateTimeOffset time)
        {
            return time.ToString("O");
        }

        public static FulcrumException ToNexusException(this Exception exception)
        {
            switch (exception)
            {
                case BusinessRuleException e:
                    switch (e.Code)
                    {
                        case 1:
                            return new FulcrumBusinessRuleException("The applicant has already been qualified", e);
                        case 2:
                            return new FulcrumBusinessRuleException("The application has already been withdrawn or rejected.", e);
                        default:
                            return new FulcrumBusinessRuleException($"Failed CRM business rule: {e.Message}", e);
                    }
                    
                case ProgrammersErrorException e:
                    return new FulcrumResourceException($"The CRM system had an internal error: {e.Message}", e);
                default:
                    return new FulcrumAssertionFailedException($"Could not convert an exception of type {exception.GetType().FullName} to a Nexus exception.");
            }
        }
    }
}
