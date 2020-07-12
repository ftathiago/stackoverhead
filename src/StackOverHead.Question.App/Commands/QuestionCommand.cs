using System.Collections.Generic;
using MediatR;
using StackOverHead.Question.App.Models;

namespace StackOverHead.Question.App.Command
{
    public class QuestionCommand : IRequest<IEnumerable<SearchQuestionResponse>>
    {
        public QuestionCommand(string content, string tags, int page, int pageSize)
        {
            Content = content;
            Tags = tags;
            Page = page;
            PageSize = pageSize;
        }

        public string Content { get; }
        public string Tags { get; }
        public int Page { get; }
        public int PageSize { get; }
    }
}