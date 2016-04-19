using System.Linq;
using System.Web.Mvc;
using R3.DataStorage;
using R3.Filters;
using R3.Helpers;
using R3.Models;
using R3.Service;

namespace R3.Controllers
{
    [CustomActionFilter]
    public class HomeController : Controller
    {
        private readonly RealEstateService realEstateService = new RealEstateService();
        private readonly MainStorageRepository mainStorageRepository = new MainStorageRepository();
        private readonly MainRequestSender mainRequestSender = new MainRequestSender();

        public ActionResult Index()
        {
            var userIpAddress = Request.ServerVariables["REMOTE_ADDR"];

            if (userIpAddress == "::1" || userIpAddress == "173.231.116.114" || userIpAddress == "69.165.203.106")
            {
                return RedirectToAction("MainListing","Home");
            }
            
            ViewBag.Message = "Parser";

            return View();
        }

        public ActionResult MainListing()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


        public ActionResult CollectData()
        {

            var searchResult = mainRequestSender.SendPostRequest();

            //analyzer.GetPriceValues(searchResult.Results);

            mainStorageRepository.SaveData(searchResult.Results);

            ViewBag.Message = "Collect today's listing from web site";

            return View("Index");
        }


        public JsonResult GetAllRecords()
        {
            var result = mainStorageRepository.GetAllRecords().ConvertAll(x => new RealEstateViewModel
            {
                Id = x.Id,
                DateTaken = x.DateTaken,
                MlsId = x.MlsId,
                MlsNumber = x.MlsNumber,
                Price = x.Price,
                YearBuild = x.YearBuild,
                NeighbourhoodName = x.NeighbourhoodName,
                Bedrooms = x.Bedrooms,
                Bathrooms = x.Bathrooms,
                Type = x.Type,
                ParkingType = x.ParkingType,
                AddressText = x.AddressText,
                PublicRemarks = x.PublicRemarks,
                RelativeDetailsURL = x.RelativeDetailsURL,
                AlternateURL = x.AlternateURL,
                PriceCoefficient = x.PriceCoefficient,
                Status = x.Status
            }).ToList();

            realEstateService.AddAnalysisData(ref result);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApplicationStatus()
        {
            var result = mainStorageRepository.GetApplicationStatus();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SetStatus(string index, string status)
        {
            bool result = realEstateService.SetStatus(index, status);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
