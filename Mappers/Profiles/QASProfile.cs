namespace QuestionsAskingServer.Mappers.Profiles
{
    using AutoMapper;
    using QuestionsAskingServer.Dtos;
    using QuestionsAskingServer.Models;

    public class QASProfile : Profile
    {
        public QASProfile()
        {
            CreateMap<CreateQuestionDto, Question>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
            .ForMember(dest => dest.CorrectAnswerId, opt => opt.MapFrom(src => src.CorrectAnswerId));

            CreateMap<CreateAnswerDto, Answer>();

            CreateMap<QueryParametersDto, QueryParameters>();
            CreateMap<QueryParameters, QueryParametersDto>();

            CreateMap<Question, QuestionDto>()    
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
                .ForMember(dest => dest.CorrectAnswerId, opt => opt.MapFrom(src => src.CorrectAnswerId));

            CreateMap<QuestionDto, Question>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
                .ForMember(dest => dest.CorrectAnswerId, opt => opt.MapFrom(src => src.CorrectAnswerId));

            CreateMap<Answer, AnswerDto>();

            CreateMap<Question, CreateQuestionDto>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
                .ForMember(dest => dest.CorrectAnswerId, opt => opt.MapFrom(src => src.CorrectAnswerId));

            CreateMap<Answer, CreateAnswerDto>();
        }
    }
}
