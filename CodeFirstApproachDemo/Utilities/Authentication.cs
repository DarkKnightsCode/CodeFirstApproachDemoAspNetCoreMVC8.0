using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeFirstApproachDemo.Utilities;
public class Authentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Session.GetString("UserName") == null)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                {"Controller","Home"},
                {"Action","Login"}
            });
        }
    }
}
