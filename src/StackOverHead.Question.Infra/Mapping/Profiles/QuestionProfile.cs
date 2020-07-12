using AutoMapper;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Mapping.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionEntity, QuestionModel>();
            CreateMap<QuestionModel, QuestionEntity>();
        }
    }
}