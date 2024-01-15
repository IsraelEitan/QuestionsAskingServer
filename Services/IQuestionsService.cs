using QuestionsAskingServer.Dtos;
using QuestionsAskingServer.Models;

namespace QuestionsAskingServer.Services
{
    public interface IQuestionsService
    {
        Task<(IEnumerable<Question> Questions, int TotalItems)> GetAllQuestionsAsync(QueryParameters parameters);
        Task<Question> GetQuestionByIdAsync(int id);
        Task<int> CreateQuestionAsync(Question createQuestionDto);
        Task UpdateQuestionAsync(int id, Question question);
        Task DeleteQuestionAsync(int id);
        Task<IEnumerable<int>> GetAllQuestionsTypes();
        Task<(int VotesCount, bool isAnswerValid)> VoteAnswerAsync(int questionId, int answerId);
    }
}
