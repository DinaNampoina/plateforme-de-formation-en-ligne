using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OnelineLearning.Exceptions
{
    public class ExcHandler : IExceptionHandler
    {
        private readonly ILogger<ExcHandler> _logger;

        public ExcHandler(ILogger<ExcHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
        {
            _logger.LogError(exception,"Une erreur inattendue est survenue ");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erreur interne du serveur",
                Detail = "Une erreur inattendue est survenue Veuillez réessayer plus tard ",
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["traceId"] =httpContext.TraceIdentifier;

            httpContext.Response.StatusCode =StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
            return true;
        }
    }
}