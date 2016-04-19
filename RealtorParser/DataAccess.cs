using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using RealtorParser.Parsers;
using RealtorParser.Senders;

namespace RealtorParser
{
    class DataAccess
    {
        protected static IMongoClient Client = new MongoClient();
        protected static IMongoDatabase Database = Client.GetDatabase("TestDB");

        private readonly DetailsSender getDetailsSender = new DetailsSender();
        private readonly HtmlParser htmlParser = new HtmlParser();

        public void SaveData(List<Result> results)
        {
            List<BsonDocument> document = new List<BsonDocument>();

            foreach (var result in results)
            {
                var mlsId = result.Id; //Id	"15518630"	string
                var mlsNumber = result.MlsNumber;//	"11568924"	string
                var postalCode = result.PostalCode;//	"H9B1M1"	string
                var bathroomTotal = result.Building.BathroomTotal;//	"1"	string
                var bedrooms = result.Building.Bedrooms;//	"4"	string
                var sizeExterior = result.Building.SizeExterior;//	null	string
                var sizeInterior = result.Building.SizeInterior;//	"1 sqft"	string
                var storiesTotal = result.Building.StoriesTotal;//	null	string
		        var type = result.Building.Type;//	"Apartment"	string

                var sizeFrontage = result.Land.SizeFrontage;//	"1 ft"	string
                var sizeTotal = result.Land.SizeTotal;//	"1.00X1.00"	string

		        var addressText = result.Property.Address.AddressText;//	"451 Rue Hyman|Dollard-Des Ormeaux, Quebec H9B1M1"	string
                var latitude = result.Property.Address.Latitude;//	"45.49336279"	string
                var longitude = result.Property.Address.Longitude;//	"-73.79517093"	string

                var price = result.Property.Price;//	"$205,000"	string
                //var Type = result.Property.Type;//	"Single Family"	string

                var publicRemarks = result.PublicRemarks;//	"Large 4 bed 1.5bath corner unit. Handyman's delight with room to let your investment grow. Shared living and dinning room, large kitchen and dinette on main level. Parquetry floors throughout, tile in kitchen, bathroom, and basement. Family friendly neighbourhood surrounded by amenities and shared complex swimming pool."	string
                var relativeDetailsURL = "http://www.realtor.ca" + result.RelativeDetailsURL;

                string alternateURL = "";
                if (result.AlternateURL != null && result.AlternateURL.DetailsLink != null)
                    alternateURL = result.AlternateURL.DetailsLink;

                var details = htmlParser.Parse(getDetailsSender.MakeRequests(relativeDetailsURL));
                var builtinValue = htmlParser.GetYear(details);
                var neighbourhoodName = htmlParser.GetNeighbourhoodName(details);
                var parkingType = htmlParser.GetParkingType(details);

                document.Add(new BsonDocument
                             {
                                {"dateTaken" , DateTime.Today},
                                {"mlsId" , mlsId},
                                {"mlsNumber" , mlsNumber},
                                {"price" , price },
                                {"year" , builtinValue},
                                {"neighbourhoodName" , neighbourhoodName},
                                {"bedrooms", bedrooms},
                                {"bathrooms", bathroomTotal},
                                {"type", type},
                                {"parkingType", parkingType},
                                {"addressText", addressText},
                                {"publicRemarks", publicRemarks},
                                {"relativeDetailsURL", relativeDetailsURL},
                                {"alternateURL", alternateURL},
                                {"priceCoefficient", 0}
                             });
            }


            SaveDocuments(document);
        }

        private async void SaveDocuments(List<BsonDocument> document)
        {
            var collection = Database.GetCollection<BsonDocument>("MainStorage");
            await collection.InsertManyAsync(document);
        }

        public async Task<List<BsonDocument>> GetAllDocuments()
        {
            var collection = Database.GetCollection<BsonDocument>("MainStorage");
            var filter = new BsonDocument();

            filter = new BsonDocument("bedrooms", new BsonDocument("$gte", 1));
            var documents = await collection.FindAsync(filter);

            List<BsonDocument> result = await collection.Find(filter).ToListAsync();

            return result;
        }
    }
}
