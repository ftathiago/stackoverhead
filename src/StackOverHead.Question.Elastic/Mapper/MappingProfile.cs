using AutoMapper;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Answer, SearchQuestionResponse>();
            CreateMap<SearchQuestionResponse, Answer>();

            CreateMap<RegisteredQuestion, Answer>()
                .ForMember(src => src.AnswerKind, option => option
                    .MapFrom(_ => AnswerKind.QuestionBody));

            CreateMap<RegisteredQuestionComment, Answer>()
                .ForMember(dest => dest.AnswerKind, option => option
                    .MapFrom(_ => AnswerKind.Comment))
                .ReverseMap();

            CreateMap<RegisteredAnswer, Answer>()
                .ForMember(dest => dest.AnswerKind, option => option
                    .MapFrom(_ => AnswerKind.Answer));

            CreateMap<RegisteredAnswerComment, Answer>()
                .ForMember(dest => dest.AnswerKind, option => option
                    .MapFrom(_ => AnswerKind.Comment));
        }
    }
}