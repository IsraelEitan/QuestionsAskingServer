namespace QuestionsAskingServer.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using QuestionsAskingServer.Dtos;
    using QuestionsAskingServer.Exceptions;
    using QuestionsAskingServer.Models;
    using QuestionsAskingServer.Services;
    using Swashbuckle.AspNetCore.Annotations;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Operations related to managing questions and answers")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionService;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionsService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all questions.
        /// </summary>
        /// <param name="parametersDto">Query parameters (optional).</param>
        /// <returns>A list of questions.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all questions")]
        [SwaggerResponse(200, "Successfully retrieved the list of questions.")]  
        public async Task<IActionResult> GetAllQuestions([FromQuery] QueryParametersDto parametersDto)
        {
            var parameters = _mapper.Map<QueryParameters>(parametersDto);
            var questionsData = await _questionService.GetAllQuestionsAsync(parameters);
            var questionsDto = _mapper.Map<IEnumerable<QuestionDto>>(questionsData.Questions);

            return Ok(new PagedResultResponse<QuestionDto>
            {
                Data = questionsDto,
                TotalCount = questionsData.TotalItems,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            });
        }

        /// <summary>
        /// Retrieves a specific question by its ID.
        /// </summary>
        /// <returns>question by its ID</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a specific question by ID")]
        [SwaggerResponse(200, "Successfully retrieved the question.")]
        [SwaggerResponse(404, "Question not found.")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            var questionDto = _mapper.Map<QuestionDto>(question);
            return Ok(questionDto);           
        }

        /// <summary>
        /// Creates a new question.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new question")]
        [SwaggerResponse(201, "Successfully created the question.")]
        [SwaggerResponse(400, "Invalid request data.")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest createQuestionDto)
        {
            // Validate the QuestionType value in the DTO
            var validQuestionTypeIds = await _questionService.GetAllQuestionsTypes();

            if (!validQuestionTypeIds.Contains(createQuestionDto.QuestionType))
            {
                throw new InvalidInputException("Question's type is not valid");
            }

            var question = _mapper.Map<Question>(createQuestionDto);
            var createdQuestionId = await _questionService.CreateQuestionAsync(question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestionId }, createQuestionDto);
        }

        /// <summary>
        /// Votes for an answer to a question.
        /// </summary>
        /// <returns>Number of votes per answer</returns> 
        [HttpPost("{questionId}/vote/{answerId}")]
        [SwaggerOperation(Summary = "Vote for an answer to a question")]
        [SwaggerResponse(200, "Vote registered successfully.")]
        [SwaggerResponse(400, "Invalid request data.")]
        [SwaggerResponse(404, "Question not found.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<IActionResult> VoteAnswer(int questionId, int answerId)
        {
            var votesCount = await _questionService.VoteAnswerAsync(questionId, answerId);
            var votesDto = new VotesCountDto()
            {
                VotesCount = votesCount.VotesCount,
                IsCorrect = votesCount.isAnswerValid
            };

            return Ok(votesDto);            
        }
    }

}
