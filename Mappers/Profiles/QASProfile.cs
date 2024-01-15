namespace QuestionsAskingServer.Mappers.Profiles
{
    using AutoMapper;
    using QuestionsAskingServer.Dtos;
    using QuestionsAskingServer.Enums;
    using QuestionsAskingServer.Models;
    using QuestionsAskingServer.Utils;

    public class QASProfile : Profile
    {
        public QASProfile()
        {
            CreateMap<int, QuestionType>().ConvertUsing(source => EnumConversionUtil.ConvertIntToEnum<QuestionType>(source));
            CreateMap<QuestionType, int>().ConvertUsing(source => (int)source);

            CreateMap<CreateQuestionRequest, Question>()
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

            CreateMap<Question, CreateQuestionRequest>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
                .ForMember(dest => dest.CorrectAnswerId, opt => opt.MapFrom(src => src.CorrectAnswerId));

            CreateMap<Answer, CreateAnswerDto>();
        }
    }
}
