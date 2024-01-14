namespace QuestionsAskingServer.Dtos
{
    public record CreateQuestionDto(string Text,string QuestionType, List<CreateAnswerDto> Answers, int? CorrectAnswerId);
}
