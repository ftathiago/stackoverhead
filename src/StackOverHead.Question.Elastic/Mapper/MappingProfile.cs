using AutoMapper;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Answer, SearchQuestionResponse>();
            CreateMap<SearchQuestionResponse, Answer>();
        }
    }
}