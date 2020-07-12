using System;

namespace StackOverHead.Question.Elastic.Exceptions
{
    public class DocumentNotFoundElkException : ElkException
    {
        public DocumentNotFoundElkException() : base()
        {
        }

        public DocumentNotFoundElkException(Guid documentId) :
            base(message: $"Document {documentId} not found to edit operation")
        { }

        public DocumentNotFoundElkException(string message) : base(message)
        {
        }

        public DocumentNotFoundElkException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}