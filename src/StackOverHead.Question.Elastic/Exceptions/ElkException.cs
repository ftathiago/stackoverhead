using System;

namespace StackOverHead.Question.Elastic.Exceptions
{
    public class ElkException : Exception
    {
        public ElkException() : base()
        {
        }

        public ElkException(string message) : base(message)
        {
        }

        public ElkException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}