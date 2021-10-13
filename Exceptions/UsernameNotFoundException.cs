using System;
using System.Runtime.Serialization;

namespace Stregsystem.Exceptions
{
    [Serializable]
    internal class UsernameNotFoundException : Exception
    {
        public UsernameNotFoundException()
        {
        }

        public UsernameNotFoundException(string message) : base(message)
        {
        }

        public UsernameNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsernameNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}