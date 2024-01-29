namespace QuestionsAskingServer.Dtos
{
    public record AnswerDto
    {
        public int Id { get; init; }
        public string Text { get; init; } = null!;
    }
}
