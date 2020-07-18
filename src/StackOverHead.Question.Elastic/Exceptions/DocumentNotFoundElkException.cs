using System;
using System.Runtime.Serialization;

namespace StackOverHead.Question.Elastic.Exceptions
{
    [Serializable]
    public class DocumentNotFoundElkException : ElkException
    {
        public DocumentNotFoundElkException() { }

        public DocumentNotFoundElkException(string message) : base(message)
        { }

        public DocumentNotFoundElkException(Guid documentId) :
            base(message: $"Document {documentId} not found to edit operation")
        { }

        public DocumentNotFoundElkException(
            string message,
            Exception inner) : base(message, inner)
        { }

        protected DocumentNotFoundElkException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        { }
    }
}