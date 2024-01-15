namespace QuestionsAskingServer.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IQuestionRepository Questions { get; }
        Task CompleteAsync();
    }
}
