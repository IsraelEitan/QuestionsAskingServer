namespace QuestionsAskingServer.Repositories
{
    public interface IUnitOfWork
    {
        IQuestionRepository Questions { get; }
        Task CompleteAsync();
    }
}
