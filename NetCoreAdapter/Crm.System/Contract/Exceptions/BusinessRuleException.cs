using System;

namespace Crm.System.Contract.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public int Code { get; }

        public BusinessRuleException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
