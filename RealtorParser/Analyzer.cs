using System.Collections.Generic;

namespace RealtorParser
{
    public class Analyzer
    {
        public int GetPriceValue(Result item)
        {
            var mlsId = item.Id; //Id	"15518630"	string
            var mlsNumber = item.MlsNumber;//	"11568924"	string
            var postalCode = item.PostalCode;//	"H9B1M1"	string
            //var detailsLink = item.AlternateURL.DetailsLink; //	"http://passerelle.centris.ca/redirect.aspx?CodeDest=BLUEHAT%26NoMLS=MT11568924&Lang=E"	string
            var bathroomTotal = item.Building.BathroomTotal;//	"1"	string
            var bedrooms = item.Building.Bedrooms;//	"4"	string
            var sizeExterior = item.Building.SizeExterior;//	null	string
            var sizeInterior = item.Building.SizeInterior;//	"1 sqft"	string
            var storiesTotal = item.Building.StoriesTotal;//	null	string
		    var type = item.Building.Type;//	"Apartment"	string

            var sizeFrontage = item.Land.SizeFrontage;//	"1 ft"	string
            var sizeTotal = item.Land.SizeTotal;//	"1.00X1.00"	string

		    var addressText = item.Property.Address.AddressText;//	"451 Rue Hyman|Dollard-Des Ormeaux, Quebec H9B1M1"	string
            var latitude = item.Property.Address.Latitude;//	"45.49336279"	string
            var longitude = item.Property.Address.Longitude;//	"-73.79517093"	string

            var price = item.Property.Price;//	"$205,000"	string
            var Type = item.Property.Type;//	"Single Family"	string

            var publicRemarks = item.PublicRemarks;//	"Large 4 bed 1.5bath corner unit. Handyman's delight with room to let your investment grow. Shared living and dinning room, large kitchen and dinette on main level. Parquetry floors throughout, tile in kitchen, bathroom, and basement. Family friendly neighbourhood surrounded by amenities and shared complex swimming pool."	string
            var relativeDetailsURL = item.RelativeDetailsURL;


            return 0;
        }



        public int GetPriceValues(List<Result> items)
        {
            foreach (var item in items)
            {

                GetPriceValue(item);

            }

            return 0;
        }
    }
}