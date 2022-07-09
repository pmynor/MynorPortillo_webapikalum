using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiKalum.Models;

namespace WebApiKalum.Utilities
{
    public class ErrorFilterException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiResponse apiResponse = new ApiResponse(){TipoError = "Error:", HttpStatusCode = "503", Mensaje = context.Exception.Message};
            context.Result = new JsonResult(apiResponse);
            base.OnException(context);
        }
    }
}