using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StackOverHead.Question.App.Command;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Elastic.Repositories;
using AutoMapper;
using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.Handlers
{
    public class SearchQuestionHandler : IRequestHandler<QuestionCommand, IEnumerable<SearchQuestionResponse>>
    {
        private readonly IAnswerRepository _answers;
        private readonly IMapper _mapper;

        public SearchQuestionHandler(
            IAnswerRepository answers,
            IMapper mapper)
        {
            _answers = answers;
            _mapper = mapper;
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
            });
        }
    }
}