using System;
using System.Web.Mvc;
using R3.DataStorage;
using R3.DataStorage.TextStorage;
using R3.Filters;

namespace R3.Controllers
{
    [CustomActionFilter]
    public class AccessLogsController : Controller
    {
        readonly AccessLogsRepository accessLogsRepository = new AccessLogsRepository();

        public ActionResult Index()
        {
            var conDb = new DataText { Path = AppDomain.CurrentDomain.BaseDirectory +  "database.dat" };
            int temp = conDb.Entries();

            return View("Index");
        }

        public JsonResult GetAllAccessLogs()
        {
            var result = accessLogsRepository.GetAllAccessLogs();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
