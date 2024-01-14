using Microsoft.EntityFrameworkCore;
using QuestionsAskingServer.Data;
using QuestionsAskingServer.Exceptions;

namespace QuestionsAskingServer.Repositories
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly QASDBContext _context;
        protected readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(QASDBContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _logger = logger; 
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await ExecuteWithExceptionHandlingAsync(
               async () => await _context.Set<T>().ToListAsync(),
               nameof(GetAllAsync)
           );
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await ExecuteWithExceptionHandlingAsync(
               async () =>
               {
                   var entity = await _context.Set<T>().FindAsync(id);
                   if (entity == null)
                   {
                       throw new EntityNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
                   }
                   return entity;
               },
               nameof(GetByIdAsync)
           );
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await ExecuteWithExceptionHandlingAsync(
                async () => await _context.Set<T>().AddAsync(entity),
                nameof(CreateAsync)
            );
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await ExecuteWithExceptionHandlingAsync(
                () =>
                {
                    _context.Set<T>().Update(entity);
                    return Task.CompletedTask;
                },
                nameof(UpdateAsync)
            );
        }
        public async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await ExecuteWithExceptionHandlingAsync(
                () =>
                {
                    _context.Set<T>().Remove(entity);
                    return Task.CompletedTask;
                },
                nameof(DeleteAsync)
            );
        }

        protected async Task<TResult> ExecuteWithExceptionHandlingAsync<TResult>(Func<Task<TResult>> operation, string operationName)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName} for entity type {EntityType}.", operationName, typeof(T).Name);
                throw;
            }
        }

        protected async Task ExecuteWithExceptionHandlingAsync(Func<Task> operation, string operationName)
        {
            try
            {
                await operation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName} for entity type {EntityType}.", operationName, typeof(T).Name);
                throw;
            }
        }

    }
}
