using QuestionsAskingServer.Dtos;
using QuestionsAskingServer.Models;

namespace QuestionsAskingServer.Repositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsWithDetailsAsync(QueryParameters parameters);
        Task<Question> GetQuestionWithDetailsByIdAsync(int id);
    }
}
