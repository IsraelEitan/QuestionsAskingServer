namespace QuestionsAskingServer.Repositories
{
    using Microsoft.Extensions.Logging;
    using QuestionsAskingServer.Data;
    using QuestionsAskingServer.Repositories.Interfaces;
    using System;
    using System.Threading.Tasks;

    internal class QuestionsUnitOfWork : IUnitOfWork
    {
        private readonly QASDBContext _context;
        private readonly ILogger<QuestionsUnitOfWork> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private IQuestionRepository _questions;
      
        public QuestionsUnitOfWork(QASDBContext context, ILogger<QuestionsUnitOfWork> logger, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        public IQuestionRepository Questions => _questions ??= new QuestionsRepository(_context, _loggerFactory.CreateLogger<QuestionsRepository>());
        
        public async Task CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Unit of work completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while completing the unit of work.");
                throw;
            }
        }
    }
}
