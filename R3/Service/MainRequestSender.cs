using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace R3.Service
{
    public class MainRequestSender
    {
        public List<Result> SendPostRequest()
        {
            List<Result> finalList = new List<Result>();
            int currentPage = 1;
            int totalPages = 2;

            while (currentPage <= totalPages)
            {
                var response = SendPstRequest(200, currentPage);

                try
                {
                    var result = JsonConvert.DeserializeObject<SearchResult>(response);
                    totalPages = result.Paging.TotalPages;

                    finalList.AddRange(result.Results);

                }
                catch (Exception)
                {
                    return null;
                }

                currentPage++;
            }

            return finalList;
        }

        private string SendPstRequest(int recordsPerPage, int currentPage)
        {
            HttpWebResponse response;
            string responseText = "";

           // var body = "CultureId=1&ApplicationId=1" +
           //"&RecordsPerPage=1000&MaximumResults=1000" +
           //"&PropertyTypeId=300&TransactionTypeId=2" +
           //"&LongitudeMin=-73.97479793908946&LongitudeMax=-73.70305797937266" +
           //"&LatitudeMin=45.42883754537223&LatitudeMax=45.529220960688704" +
           //"&SortOrder=A&SortBy=1" +
           //"&PriceMin=100000&PriceMax=400000" +
           //"&BuildingTypeId=1" +
           //"&PolygonPoints=-73.86676112076171+45.49620752476158%2C-73.87696755522353+45.480402341184416%2C-73.89928353422744+45.46475324875623%2C-73.88572228544814+45.44006700490998%2C-73.85344994658095+45.425250071921845%2C-73.7792922317372+45.470652263077326%2C-73.74753487700087+45.49965679751488%2C-73.79363337417968+45.51557686414152%2C-73.84770670791991+45.51882448589523%2C-73.86676112076171+45.49620752476158" +
           //"&PolyZoomLevel=13" +
           //"&BedRange=3-0&BathRange=0-0" +
           // "&ParkingSpaceRange=0-0" +
           //"&viewState=m&Longitude=-73.6919889&Latitude=45.448025" +
           //"&ZoomLevel=11" +
           //"&CurrentPage=1";


            string body = "CultureId=1&ApplicationId=1" +
                          "&RecordsPerPage=" + recordsPerPage +
                          "&MaximumResults=500" +
                          "&PropertySearchTypeId=300&TransactionTypeId=2" +
                          "&BuildingTypeId=1" +
                          "&StoreyRange=0-0" +
                          "&BedRange=3-0" +
                          "&BathRange=0-0" +
                          "&LongitudeMin=-73.99278483234194&LongitudeMax=-73.72104487262514&LatitudeMin=45.435791811920254&LatitudeMax=45.53616284804593" +
                          "&SortOrder=A&SortBy=1" +
                          "&PriceMin=100000&PriceMax=400000" +
                          "&PolygonPoints=-73.86676112076171+45.49620752476158%2C-73.87696755522353+45.480402341184416%2C-73.89928353422744+45.46475324875623%2C-73.88572228544814+45.44006700490998%2C-73.85344994658095+45.425250071921845%2C-73.7792922317372+45.470652263077326%2C-73.78388576302788+45.490728361837846%2C-73.81255321297905+45.497827907114%2C-73.84600907130597+45.495751008983476%2C-73.86676112076171+45.49620752476158" +
                          "%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158%2C-73.86676112076171+45.49620752476158" +
                          "&PolyZoomLevel=13&viewState=m" +
                          "&Longitude=-73.6271953582763&Latitude=45.4260459477165" +
                          "&ZoomLevel=14" +
                          "&CurrentPage=" + currentPage +
                          "&PropertyTypeGroupID=1";

            if (Request_www_realtor_ca(body, out response))
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseText = reader.ReadToEnd();
                }

                response.Close();
            }

            return responseText;
        }

        private bool Request_www_realtor_ca(string body, out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api2.realtor.ca/Listing.svc/PropertySearch_Post");

                request.KeepAlive = true;
                request.Accept = "*/*";
                request.Headers.Add("Origin", @"https://www.realtor.ca");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.63 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Referer = "https://www.realtor.ca/";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8,ru;q=0.6,ro;q=0.4");

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                byte[] postBytes = Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }
    }
}