using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace R3.Helpers
{
    public class MainRequestSender
    {
        public SearchResult SendPostRequest()
        {
            var result = SendPstRequest();

            return JsonConvert.DeserializeObject<SearchResult>(result);
        }

        private string SendPstRequest()
        {
            HttpWebResponse response;
            string responseText = "";

            var body = "CultureId=1&ApplicationId=1" +
                       "&RecordsPerPage=1000&MaximumResults=1000" +
                       "&PropertyTypeId=300&TransactionTypeId=2" +
                       "&LongitudeMin=-73.97479793908946&LongitudeMax=-73.70305797937266" +
                       "&LatitudeMin=45.42883754537223&LatitudeMax=45.529220960688704" +
                       "&SortOrder=A&SortBy=1" +
                       "&PriceMin=100000&PriceMax=400000" +
                       "&BuildingTypeId=1" +
                       "&PolygonPoints=-73.86676112076171+45.49620752476158%2C-73.87696755522353+45.480402341184416%2C-73.89928353422744+45.46475324875623%2C-73.88572228544814+45.44006700490998%2C-73.85344994658095+45.425250071921845%2C-73.7792922317372+45.470652263077326%2C-73.74753487700087+45.49965679751488%2C-73.79363337417968+45.51557686414152%2C-73.84770670791991+45.51882448589523%2C-73.86676112076171+45.49620752476158" +
                       "&PolyZoomLevel=13"+
                       "&BedRange=3-0&BathRange=0-0" +
                       //"&ParkingSpaceRange=0-0" +
                       "&viewState=m&Longitude=-73.6919889&Latitude=45.448025" +
                       "&ZoomLevel=11" +
                       "&CurrentPage=1";

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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.realtor.ca/api/Listing.svc/PropertySearch_Post");

                request.KeepAlive = true;
                request.Accept = "*/*";
                request.Headers.Add("Origin", @"https://www.realtor.ca");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Referer = "https://www.realtor.ca/Residential/Map.aspx";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8,ru;q=0.6,ro;q=0.4");
                request.Headers.Set(HttpRequestHeader.Cookie, @"GUID=a5d4c09e-aa25-4846-a54c-e61f77f97006; TermsOfUseAgreement=ACCEPTED; fsr.r=%7B%22d%22%3A90%2C%22i%22%3A%22d464cf5-82675489-babf-9372-97314%22%2C%22e%22%3A1443451598232%7D; previouslyViewedListings=15916963~15624716~15690121~15530373~16054760~16128269~16179189~16192346~16116354~15968962~15916975~15503700~; ICXCookieConversion=1; comm_tab_hint=1; _ga=GA1.2.1544596104.1442846674; __utma=134851954.1544596104.1442846674.1446433945.1446472918.36; __utmz=134851954.1446037712.34.16.utmcsr=r3.somee.com|utmccn=(referral)|utmcmd=referral|utmcct=/Home/MainListing; ASP.NET_SessionId=2kbl3fkdxwzskohupvwws115; SHOW_ADV_HME_SRCH=1; Language=1; app_mode=1; cookieTest=val; cookieTestCP=val; PolygonPoints=-73.86676112076171%2045.49620752476158%2C-73.87696755522353%2045.480402341184416%2C-73.89928353422744%2045.46475324875623%2C-73.88572228544814%2045.44006700490998%2C-73.85344994658095%2045.425250071921845%2C-73.7792922317372%2045.470652263077326%2C-73.74753487700087%2045.49965679751488%2C-73.79363337417968%2045.51557686414152%2C-73.84770670791991%2045.51882448589523%2C-73.86676112076171%2045.49620752476158%2C-73.86676112076171%2045.49620752476158%2C-73.86676112076171%2045.49620752476158%2C-73.86676112076171%2045.49620752476158%2C-73.86676112076171%2045.49620752476158%2C-73.86676112076171%2045.49620752476158; PolyZoomLevel=13");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

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