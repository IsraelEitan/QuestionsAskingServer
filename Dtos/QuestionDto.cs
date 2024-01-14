namespace QuestionsAskingServer.Dtos
{
    public record QuestionDto
    {
        public int Id { get; init; }
        public string Text { get; init; }
        public string QuestionType { get; init; }
        public int? CorrectAnswerId { get; init; }
        public List<AnswerDto> Answers { get; init; }
    }
}
