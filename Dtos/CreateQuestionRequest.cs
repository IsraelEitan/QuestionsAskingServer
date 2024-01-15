namespace QuestionsAskingServer.Dtos
{
    public record CreateQuestionRequest(string Text,int QuestionType, List<CreateAnswerDto> Answers, int? CorrectAnswerId);
}
