using System;
using System.Runtime.Serialization;

namespace Stregsystem.Exceptions
{
    [Serializable]
    internal class TransactionExecuteFailureException : Exception
    {
        public TransactionExecuteFailureException()
        {
        }

        public TransactionExecuteFailureException(string message) : base(message)
        {
        }

        public TransactionExecuteFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TransactionExecuteFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}