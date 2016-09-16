using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using R3.DataStorage.LiteDB;
using R3.DataStorage.Tables;
using R3.Models;
using R3.Service;

namespace R3.DataStorage
{
    public class MainStorageRepository
    {
        private readonly DetailsSender getDetailsSender = new DetailsSender();
        private readonly HtmlParser htmlParser = new HtmlParser();


        public List<RealEstate> GetAllRecords()
        {           
            ArchiveSoldRealEstates();

            using (var db = new MainStorage())
            {
                var maxValue = db.RealEstates.Max(x => x.DateTaken);         
     
                var query = db.RealEstates.Where(x => !x.Status.Equals("No"))
                                        .Where(x => !x.Type.Equals("Appartment"))
                                        .Where(x => x.DateTaken == maxValue);

                return query.ToList();
            }
        }

        public void SaveData(List<Result> results)
        {
            List<RealEstate> document = new List<RealEstate>();

            foreach (var result in results)
            {
                var mlsId = result.Id; //Id	"15518630"	string
                var mlsNumber = result.MlsNumber;//	"11568924"	string
                //var postalCode = result.PostalCode;//	"H9B1M1"	string
                var bathroomTotal = Convert.ToInt32(result.Building.BathroomTotal);//	"1"	string
                var bedrooms = Convert.ToInt32(result.Building.Bedrooms);//	"4"	string
                //var sizeExterior = result.Building.SizeExterior;//	null	string
                //var sizeInterior = result.Building.SizeInterior;//	"1 sqft"	string
                //var storiesTotal = result.Building.StoriesTotal;//	null	string
                var type = result.Building.Type;//	"Apartment"	string

                //var sizeFrontage = result.Land.SizeFrontage;//	"1 ft"	string
                //var sizeTotal = result.Land.SizeTotal;//	"1.00X1.00"	string

                var addressText = result.Property.Address.AddressText;//	"451 Rue Hyman|Dollard-Des Ormeaux, Quebec H9B1M1"	string
                var latitude = decimal.Parse(result.Property.Address.Latitude);//	"45.49336279"	string
                var longitude = decimal.Parse(result.Property.Address.Longitude);//	"-73.79517093"	string

                var price = result.Property.Price;//	"$205,000"	string
                //var Type = result.Property.Type;//	"Single Family"	string

                var individuals = JsonConvert.SerializeObject(result.Individual);

                var publicRemarks = result.PublicRemarks;//	"Large 4 bed 1.5bath corner unit. Handyman's delight with room to let your investment grow. Shared living and dinning room, large kitchen and dinette on main level. Parquetry floors throughout, tile in kitchen, bathroom, and basement. Family friendly neighbourhood surrounded by amenities and shared complex swimming pool."	string
                var relativeDetailsURL = "http://www.realtor.ca" + result.RelativeDetailsURL;

                string alternateURL = "";
                if (result.AlternateURL != null && result.AlternateURL.DetailsLink != null)
                    alternateURL = result.AlternateURL.DetailsLink;

                if (false) //desabled because of detected bot activity by api
                {
                    var details = htmlParser.Parse(getDetailsSender.MakeRequests(relativeDetailsURL));
                    //var builtinValue = htmlParser.GetYear(details);
                    //var neighbourhoodName = htmlParser.GetNeighbourhoodName(details);
                    //var parkingType = htmlParser.GetParkingType(details);
                }
                var builtinValue = "";
                var neighbourhoodName = GetNeighbourhoodName(addressText);
                var parkingType = "";

                document.Add(new RealEstate
                             {
                                DateTaken = DateTime.Today,
                                MlsId = mlsId,
                                MlsNumber = mlsNumber,
                                Price = price,
                                YearBuild =  builtinValue,
                                NeighbourhoodName = neighbourhoodName,
                                Bedrooms = bedrooms,
                                Bathrooms = bathroomTotal,
                                Type = type,
                                ParkingType = parkingType,
                                AddressText = addressText,
                                Latitude = latitude,
                                Longitude = longitude,
                                PublicRemarks = publicRemarks,
                                RelativeDetailsURL = relativeDetailsURL,
                                AlternateURL = alternateURL,
                                Individuals = individuals.Length <= 4000 ? individuals : individuals.Substring(0, 4000),
                                PriceCoefficient = 0
                             });
            }

            SaveRealEstates(document);            
        }

        private void SaveRealEstates(List<RealEstate> realEstates)
        {
            using (var db = new MainStorage())
            {
                foreach (var realEstate in realEstates)
                {
                    db.RealEstates.Add(realEstate);
                }
                db.SaveChanges();

                //sync status
                var updateQuery = "UPDATE T SET T.[status] = OT.[status] FROM realestate T INNER JOIN RealEstateHistory OT ON T.mlsNumber = OT.mlsNumber WHERE OT.[status] is not null AND T.[status] is null";
                db.Database.ExecuteSqlCommand(updateQuery);

                //update status for old records
                updateQuery = "UPDATE RealEstate SET STATUS = 'closed' WHERE mlsId NOT IN (SELECT mlsId FROM RealEstate WHERE dateTaken = (SELECT MAX(dateTaken) FROM RealEstate)) AND STATUS IS NULL";
                db.Database.ExecuteSqlCommand(updateQuery);

                //save sold records
                updateQuery = "INSERT INTO RealEstateSold SELECT * FROM RealEstate WHERE status = 'closed'";
                db.Database.ExecuteSqlCommand(updateQuery);

                //clean sold records
                updateQuery = "DELETE FROM RealEstate WHERE mlsNumber in (SELECT mlsNumber FROM RealEstateSold)";
                db.Database.ExecuteSqlCommand(updateQuery);

                //clean valid access logs (home and office)
                updateQuery = "DELETE FROM AccessLogs WHERE IP in ('::1', '173.231.116.114', '69.165.203.106')";
                db.Database.ExecuteSqlCommand(updateQuery);

                //clean valid access logs (Site24x7 refresh utility)
                updateQuery = "DELETE FROM AccessLogs WHERE IP in ('72.5.230.111')";
                db.Database.ExecuteSqlCommand(updateQuery);                

                //archive records
                updateQuery = "INSERT INTO RealEstateHistory (mlsNumber, dateTaken, price, status) SELECT mlsNumber, dateTaken, price, status FROM RealEstate WHERE dateTaken not in (select max(dateTaken) from RealEstate); " +
                              "DELETE FROM RealEstate WHERE dateTaken not in (select max(dateTaken) from RealEstate);";
                db.Database.ExecuteSqlCommand(updateQuery);

                //clean history records
                updateQuery = "DELETE FROM RealEstateHistory WHERE mlsNumber not in (SELECT mlsNumber FROM RealEstate)";
                db.Database.ExecuteSqlCommand(updateQuery);
            }
        }

        private void ArchiveSoldRealEstates()
        {
            List<RealEstateSold> soldRealEstates;
            using (var db = new MainStorage())
            {
                soldRealEstates = db.SoldRealEstates.ToList();    
            }

            // ReSharper disable once UnusedVariable
            List<int> idList = (from soldRealEstate in soldRealEstates where LiteDbStorage.Insert(soldRealEstate) select soldRealEstate.Id).ToList();

#if !DEBUG
            using (var db = new MainStorage())
            {
                var itemsToDelete = db.SoldRealEstates.Where(x => idList.Contains(x.Id));
                db.SoldRealEstates.RemoveRange(itemsToDelete);
                db.SaveChanges();
            }
#endif
        }

        public Dictionary<string, int> GetNumberOfHistoryRecords(List<RealEstateViewModel> realEstates)
        {
            using (var db = new MainStorage())
            {
                var query = db.RealEstateHistories.GroupBy(info => info.MlsNumber)                    
                    .Select(group => new { 
                             MlsNumber = group.Key, 
                             Count = group.Count() 
                        })
                        .Where(x => x.Count == 1)
                        .ToList();

                var result = query.Where(x => realEstates.Any(c => c.MlsNumber == x.MlsNumber));

                return result.ToDictionary(kvp => kvp.MlsNumber, kvp => kvp.Count);
            }
        }

        public bool SetStatus(string mlsId, string status)
        {
#if !DEBUG
            using (var db = new MainStorage())
            {
                var result = db.RealEstates.Where(x => x.MlsId == mlsId);

                result.ToList().ForEach(e =>{e.Status = status;});                    
                db.SaveChanges();
            }
#endif
            return true;
        }

        public ApplicationStatusViewModel GetApplicationStatus()
        {
            using (var db = new MainStorage())
            {
                var maxValue = db.RealEstates.Max(x => x.DateTaken);


                var query = db.RealEstates.Select(m => new { m.MlsId, m.Status}).Distinct()
                    .GroupBy(info => info.Status ?? "Unclear")
                    .Select(group => new
                                     {
                                         Status = group.Key,
                                         Count = group.Count()
                                     })
                    .ToList();

                var yes = query.Find(x => x.Status.Equals("Yes"));
                var no = query.Find(x => x.Status.Equals("No"));
                var maybe = query.Find(x => x.Status.Equals("Maybe"));
                var unclear = query.Find(x => x.Status.Equals("Unclear"));

                return new ApplicationStatusViewModel
                       {
                           LastDateTaken = maxValue,
                           NumberOfYes = yes == null ? 0 : yes.Count,
                           NumberOfNo = no == null ? 0 : no.Count,
                           NumberOfMaybe = maybe == null ? 0 : maybe.Count,
                           NumberOfUnclear = unclear == null ? 0 : unclear.Count,
                           TotalNumberOfRecords = query.Sum(x => x.Count),
                           //NumberOfHiddenRecords = query.Find(x => x.Status.Equals("closed")).Count
                       };
            }
        }

        public List<RealEstateSold> GetAllSoldRecords()
        {
            List<RealEstateSold> result;
            using (var db = new MainStorage())
            {
                result = db.SoldRealEstates.OrderByDescending(x=>x.DateTaken).ToList();
            }

            result.AddRange(LiteDbStorage.SelectAll());

            return result;
        }

        public Dictionary<string, int> GetPriceChangesFromHistory(List<RealEstateViewModel> results)
        {
            using (var db = new MainStorage())
            {
                var latestChanges1 = db.RealEstateHistories.ToList();

                var latestChanges = latestChanges1
                    .Where(x => results.Exists(c => c.MlsNumber.Equals(x.MlsNumber)))
                    .Where(x => results.Exists(c => !c.Price.Equals(x.Price)))
                    .GroupBy(g => g.MlsNumber)
                    .Select(
                         v =>
                             new
                             {
                                 MlsNumber = v.Key,
                                 DateTaken = v.Max(user => user.DateTaken)
                             }).ToList();

                var historyWithPrices = db.RealEstateHistories.ToList().Where(x => latestChanges.Exists(c => c.MlsNumber.Equals(x.MlsNumber) && c.DateTaken.Equals(x.DateTaken))).ToList();

                var result = historyWithPrices
                                                .Join(results, r => r.MlsNumber, h => h.MlsNumber,
                                                (r, h) => new { r, h }
                                                ).ToList()
                                                .ToDictionary(kvp => kvp.r.MlsNumber, kvp => GetPriceDifference(kvp.h.Price, kvp.r.Price));

                return result;
            }
        }

        private int GetPriceDifference(string oldPrice, string newPrice)
        {
            try
            {
                var oldPriceInt = int.Parse(oldPrice.Replace("$", "").Replace(",", ""));
                var newPriceInt = int.Parse(newPrice.Replace("$", "").Replace(",", ""));

                return oldPriceInt - newPriceInt;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private string GetNeighbourhoodName(string addressText)
        {
            var result1 = addressText.Substring(addressText.IndexOf('|') + 1, addressText.Length - addressText.IndexOf('|') - 1);
            var result = result1.Substring(0, result1.IndexOf(','));
            return result;
        }
    
    }
}