using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using RealtorParser.Senders;

namespace RealtorParser
{
    public class DataCollector
    {
        readonly GenericDetails genericDetails = new GenericDetails();
        readonly DataAccess dataAccess = new DataAccess();

        public void GetAllGenericDetails()
        {
            Task<List<BsonDocument>> documents = dataAccess.GetAllDocuments();

            var count = documents.Result.Count;


            var t = genericDetails.MakeRequests("http://passerelle.centris.ca/redirect.aspx?CodeDest=REMAX%26NoMLS=MT10784240&Lang=E");

        }
    }
}