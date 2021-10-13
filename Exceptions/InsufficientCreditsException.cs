using Stregsystem.Models;
using System;
using System.Runtime.Serialization;

namespace Stregsystem.Exceptions
{
    [Serializable]
    public class InsufficientCreditsException : Exception
    {

        public User eUser { get; set; }
        public Product eProduct { get; set; }

        public InsufficientCreditsException(User aUser, Product aProduct)
        {
            eUser = aUser;
            eProduct = aProduct;
        }

        public InsufficientCreditsException(string message) : base(message)
        {
        }

        public InsufficientCreditsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientCreditsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}