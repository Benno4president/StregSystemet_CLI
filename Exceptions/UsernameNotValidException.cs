using System;
using System.Runtime.Serialization;

namespace Stregsystem.Exceptions
{
    [Serializable]
    internal class UsernameNotValidException : Exception
    {
        public UsernameNotValidException()
        {
        }

        public UsernameNotValidException(string message) : base(message)
        {
        }

        public UsernameNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsernameNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}