using Microsoft.AspNetCore.Mvc.Filters;
using QuestionsAskingServer.Exceptions;
using QuestionsAskingServer.Utils;

namespace QuestionsAskingServer.Validators
{
    public class ModelValidationActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorDetails = ErrorHandlingService.CreateValidationErrorMessage(context.ModelState);
                throw new InvalidInputException(errorDetails);
            }

        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
