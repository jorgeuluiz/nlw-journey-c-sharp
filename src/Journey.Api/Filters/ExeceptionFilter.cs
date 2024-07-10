using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExeceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
            {
                var journeyException = context.Exception as JourneyException;
                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();

                var reponseJson = new ResponseErrorsJson(journeyException.GetErrorMessages());

                context.Result = new ObjectResult(reponseJson);
            }           
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var list = new List<string>
                {
                    "Erro desconhecido"
                };

                var reponseJson = new ResponseErrorsJson(list);
                context.Result = new ObjectResult(reponseJson);
            }
        }
    }
}
