using System.Linq;
using System.Web.Mvc;
using R3.DataStorage;
using R3.Models;

namespace R3.Controllers
{
    public class SoldInfoController : Controller
    {
        private readonly MainStorageRepository mainStorageRepository = new MainStorageRepository();

        public ActionResult Index()
        {
            return View("Index");
        }

        public JsonResult GetAllSoldRecords()
        {
            var result = mainStorageRepository.GetAllSoldRecords().ConvertAll(x => new RealEstateViewModel
            {
                Id = x.Id,
                SoldDate = x.DateTaken,
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

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
