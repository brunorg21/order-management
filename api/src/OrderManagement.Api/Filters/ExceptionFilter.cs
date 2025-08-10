using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderManagement.Communication.Responses;
using OrderManagement.Exception;

namespace OrderManagement.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is OrderManagementException)
            {
                HandleException(context);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private void HandleException(ExceptionContext context)
        {

            var orderManagementException = context.Exception as OrderManagementException;

            var errorResponse = new ResponseErrorJson(orderManagementException.GetErrors());

            context.HttpContext.Response.StatusCode = orderManagementException!.StatusCode;
            context.Result = new ObjectResult(errorResponse);
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson("Erro desconhecido.");

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
