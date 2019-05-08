using System;

namespace Crm.System.Contract.Exceptions
{
    public class ProgrammersErrorException : Exception
    {
        public ProgrammersErrorException(string message) : base(message)
        {
        }
    }
}
