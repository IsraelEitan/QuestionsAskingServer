namespace QuestionsAskingServer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using QuestionsAskingServer.Models;
    using AutoMapper;
    using QuestionsAskingServer.Helpers;
    using QuestionsAskingServer.Exceptions;
    using QuestionsAskingServer.Enums;
    using QuestionsAskingServer.Repositories.Interfaces;

    public class QuestionsService : IQuestionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly ILogger<QuestionsService> _logger;
        private readonly IMapper _mapper;

        public QuestionsService(IUnitOfWork unitOfWork, ICacheService cacheService, ILogger<QuestionsService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<Question> Questions, int TotalItems)> GetAllQuestionsAsync(QueryParameters parameters)
        {
            var cacheKey = $"questions_all_{parameters.SearchText}_{parameters.PageNumber}_{parameters.PageSize}";
            return await ExecuteWithLoggingAndReturnValueAsync (
                () => _cacheService.GetOrCreateAsync(cacheKey, async () =>
                {
                    var questions = await _unitOfWork.Questions.GetQuestionsWithDetailsAsync(parameters);
                    return ( questions, questions.Count());
                }),
            $"retrieving all question for page {parameters.PageNumber} with page size {parameters.PageSize}");
        }

        public async Task<IEnumerable<int>> GetAllQuestionsTypes()
        {
            var cacheKey = $"questionsTypes_all";
            return await ExecuteWithLoggingAndReturnValueAsync(
                () => _cacheService.GetOrCreateAsync(cacheKey, async () =>
                {
                    return await _unitOfWork.Questions.GetAllQuestionsTypes();        
                }),
            $"retrieving all questions types");
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            var cacheKey = $"question_id_{id}";
            return await ExecuteWithLoggingAndReturnValueAsync (
                () => _cacheService.GetOrCreateAsync(cacheKey, async () =>
                {
                    var question = await _unitOfWork.Questions.GetQuestionWithDetailsByIdAsync(id);
                    if (question == null)
                    {
                        throw new EntityNotFoundException($"Question with ID {id} not found.");
                    }
                    return _mapper.Map<Question>(question);
                }),
            $"retrieving question with question ID {id}");
        }

        public async Task<int> CreateQuestionAsync(Question newQuestion)
        {
            


            var question = _mapper.Map<Question>(newQuestion);

            await ExecuteWithLoggingAsync(async () =>
            {
                await _unitOfWork.Questions.CreateAsync(question);
                await _unitOfWork.CompleteAsync();

                _cacheService.Invalidate("questions_all");
            }, "creating a new invoice");

            return question.Id;
        }

       public async Task UpdateQuestionAsync(int id, Question question)
        {
            ValidateQuestionUpdate(id, question);

            await ExecuteWithLoggingAsync(async () =>
            {
                await UpdateExistingQuestionAsync(question);
                _cacheService.Invalidate("questions_all");
                _cacheService.Invalidate($"question_id_{id}");
            }, $"updating invoice with ID {id}");
        }

        public async Task DeleteQuestionAsync(int id)
        {
            await ExecuteWithLoggingAsync(async () =>
            {
                var invoice = await _unitOfWork.Questions.GetByIdAsync(id);
                if (invoice == null) throw new EntityNotFoundException("Invoice not found");

                await _unitOfWork.Questions.DeleteAsync(invoice);
                await _unitOfWork.CompleteAsync();
                _cacheService.Invalidate("questions_all");
                _cacheService.Invalidate($"question_id_{id}");
            }, $"deleting invoice with ID {id}");
        }

        public async Task<(int VotesCount, bool isAnswerValid)> VoteAnswerAsync(int questionId, int answerId)
        {
            var question = await _unitOfWork.Questions.GetQuestionWithDetailsByIdAsync(questionId);
            if (question == null)
            {
                throw new EntityNotFoundException($"Question with ID {questionId} not found.");
            }

            var answer = question.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
            {
                throw new EntityNotFoundException($"Answer with ID {answerId} not found in question with ID {questionId}.");
            }

            bool isCorrect = question.QuestionTypeId == QuestionType.Trivia && answer.Id == question.CorrectAnswerId;
       
            answer.Votes += 1;

            await _unitOfWork.Questions.UpdateAsync(question);
            await _unitOfWork.CompleteAsync();

            _cacheService.Invalidate($"question_id_{questionId}");
            
            return (answer.Votes, isCorrect);
        }

        private async Task ExecuteWithLoggingAsync(Func<Task> operation, string operationDescription)
        {
            try
            {
                await operation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error {operationDescription}.");
                throw;
            }
        }

        private async Task<T> ExecuteWithLoggingAndReturnValueAsync<T>(Func<Task<T>> operation, string operationDescription)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error {operationDescription}.");
                throw;
            }
        }

        private async Task ValidateQuestionUpdate(int id, Question question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));
            if (id != question.Id) throw new ArgumentException("ID mismatch");

            var existingQuestion = await _unitOfWork.Questions.GetByIdAsync(id);
            if (existingQuestion == null) throw new EntityNotFoundException("question not found");
        }

        private async Task UpdateExistingQuestionAsync(Question question)
        {
            question.Text = question.Text;
            question.Answers = question.Answers;

            await _unitOfWork.Questions.UpdateAsync(question);
            await _unitOfWork.CompleteAsync();
        }

    }
}