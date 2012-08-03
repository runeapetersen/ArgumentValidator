using System;
using System.Runtime.Serialization;

namespace ArgumentValidator
{
    [Serializable]
    internal sealed class ValidationException : ArgumentException
    {
        public ValidationException()
            : base() { }

        public ValidationException(string message)
            : base(message) { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException) { }

        public ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public ValidationException(string message, string paramName)
            : base(message, paramName) { }

        public ValidationException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException) { }
    }
}
