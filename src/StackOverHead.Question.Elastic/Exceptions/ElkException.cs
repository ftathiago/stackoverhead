using System;
using System.Runtime.Serialization;

namespace StackOverHead.Question.Elastic.Exceptions
{
    [Serializable]
    public class ElkException : Exception
    {
        public ElkException() : base()
        { }

        public ElkException(string message) : base(message)
        { }

        public ElkException(string message, Exception innerException) : base(message, innerException)
        { }

        protected ElkException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        { }
    }
}