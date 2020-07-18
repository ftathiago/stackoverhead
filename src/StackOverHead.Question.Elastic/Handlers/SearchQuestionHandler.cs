using AutoMapper;
using MediatR;
using StackOverHead.Question.App.Command;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Elastic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StackOverHead.Question.Elastic.Handlers
{
    public class SearchQuestionHandler : IRequestHandler<QuestionCommand, IEnumerable<SearchQuestionResponse>>
    {
        private readonly IAnswerRepository _answers;

        public SearchQuestionHandler(IAnswerRepository answers)
        {
            _answers = answers;
        }

        public async Task<IEnumerable<SearchQuestionResponse>> Handle(
            QuestionCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _answers.SearchAsync(
                request.Content,
                request.Tags,
                request.Page,
                request.PageSize);

            return response.Select(answer => new SearchQuestionResponse
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                AnswerKind = answer.AnswerKind,
                Content = answer.Content,
                Tags = answer.Tags,
            });
        }
    }
}