using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {

        private readonly ILogger _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Ocorreu uma exceção não tratada: status code 500.");

            context.Result = new ObjectResult("Ocorreu um erro interno no servidor.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
