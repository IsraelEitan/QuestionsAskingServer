namespace QuestionsAskingServer.Dtos
{
    public record VotesCountDto
    {
        public int VotesCount { get; set; }
        public bool IsCorrect { get; set; }
    }
}
