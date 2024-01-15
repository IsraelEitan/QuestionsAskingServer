namespace QuestionsAskingServer.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using QuestionsAskingServer.Data;
    using QuestionsAskingServer.Dtos;
    using QuestionsAskingServer.Exceptions;
    using QuestionsAskingServer.Models;
    using QuestionsAskingServer.Repositories.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class QuestionsRepository : BaseRepository<Question>, IQuestionRepository
    {
        private readonly QASDBContext _context;

        public QuestionsRepository(QASDBContext context, ILogger<QuestionsRepository> logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetQuestionsWithDetailsAsync(QueryParameters parameters)
        {
            var query = _context.Questions.Include(q => q.Answers).AsQueryable();

            // Search and filtering logic
            if (!string.IsNullOrEmpty(parameters.SearchText))
            {
                query = query.Where(q => q.Text.Contains(parameters.SearchText));
            }

            // Pagination logic
            if (parameters.PageNumber.HasValue && parameters.PageSize.HasValue)
            {
                query = query.Skip((parameters.PageNumber.Value - 1) * parameters.PageSize.Value)
                             .Take(parameters.PageSize.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllQuestionsTypes()
        {
            return await _context.QuestionTypes.Select(qt => (int)qt.Type).ToListAsync();     
        }

        public async Task<Question> GetQuestionWithDetailsByIdAsync(int id)
        {
            var question =  await _context.Questions
                                .AsNoTracking()
                                .Include(q => q.Answers)
                                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                throw new EntityNotFoundException($"Entity of type {typeof(Question).Name} with id {id} not found.");
            }

            return question;
        }
    }
}
