using System.Web.Mvc;
using R3.DataStorage;
using R3.Filters;

namespace R3.Controllers
{
    [CustomActionFilter]
    public class AccessLogsController : Controller
    {
        readonly AccessLogsRepository accessLogsRepository = new AccessLogsRepository();

        public ActionResult Index()
        {
            return View("Index");
        }

        public JsonResult GetAllAccessLogs()
        {
            var result = accessLogsRepository.GetAllAccessLogs();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
