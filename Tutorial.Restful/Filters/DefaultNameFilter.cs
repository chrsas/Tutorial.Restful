using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tutorial.Restful.Filters
{
    public class DefaultNameFilter : Attribute, IActionFilter, IAsyncActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionDescriptor.RouteValues["name"] = "Anonymous";
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers["X-Name"] = context.ActionDescriptor.RouteValues["name"];
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            OnActionExecuting(context);
            var result = await next();
            OnActionExecuted(result);
        }
    }
}