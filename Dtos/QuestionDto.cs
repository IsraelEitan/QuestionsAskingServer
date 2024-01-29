namespace QuestionsAskingServer.Dtos
{
    public record QuestionDto
    {
        public int Id { get; init; }
        public string Text { get; init; } = null!;
        public string QuestionType { get; init; } = null!;
        public int? CorrectAnswerId { get; init; }
        public List<AnswerDto> Answers { get; init; } = null!;
    }
}
