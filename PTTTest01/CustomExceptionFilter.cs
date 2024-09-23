using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PTTTest01.Helpers;

namespace PTTTest01
{
    public class CustomExceptionFilter(ILogger<CustomExceptionFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger = logger;

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            var response = new
            {
                Message = AvatarConstants.InternalServerErrorMessage,
                ExceptionMessage = context.Exception.Message,
                ExceptionType = context.Exception.GetType().Name
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = AvatarConstants.InternalServerErrorCode
            };

            context.ExceptionHandled = true;
        }
    }
}