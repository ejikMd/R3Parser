using System.Web.Mvc;
using R3.DataStorage;
using R3.DataStorage.Tables;

namespace R3.Filters
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            MainStorage storeDb = new MainStorage();

            AccessLog log = new AccessLog
            {
                //Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                //Action = filterContext.ActionDescriptor.ActionName + " (Logged By: Custom Action Filter)",
                Ip = filterContext.HttpContext.Request.UserHostAddress,
                AccessDate = filterContext.HttpContext.Timestamp
            };

            storeDb.AccessLogs.Add(log);
            storeDb.SaveChanges();

            OnActionExecuting(filterContext);
        }
    }
}