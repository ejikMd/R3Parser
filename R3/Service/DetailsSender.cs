using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace R3.Helpers
{
    public class DetailsSender
    {
        public string MakeRequests(string url)
        {
            HttpWebResponse response;
            string responseText = null;

            if (Request_www_realtor_ca(url, out response))
            {
                responseText = ReadResponse(response);

                response.Close();
            }

            return responseText;
        }

        private static string ReadResponse(HttpWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                Stream streamToRead = responseStream;
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    streamToRead = new GZipStream(streamToRead, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    streamToRead = new DeflateStream(streamToRead, CompressionMode.Decompress);
                }

                using (StreamReader streamReader = new StreamReader(streamToRead, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private bool Request_www_realtor_ca(string url, out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.KeepAlive = true;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";
                request.Headers.Add("DNT", @"1");
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8,ru;q=0.6,ro;q=0.4,fr;q=0.2");
                request.Headers.Set(HttpRequestHeader.Cookie, @"GUID=a5d4c09e-aa25-4846-a54c-e61f77f97006; Language=Culture=en-CA; TargetPage=/map.aspx#CultureID=1&Latitude=45.4645503&Longitude=-73.71504589999999&ZoomLevel=11&PropertyTypeId=300&TransactionTypeId=2&PriceMin=75000&PriceMax=175000&BedRange=3-0&BathRange=0-0; TermsOfUseAgreement=ACCEPTED; ASP.NET_SessionId=kkpwq3hklubwmdfgv3oljhnc; fsr.r=%7B%22d%22%3A90%2C%22i%22%3A%22d464cf5-82675489-babf-9372-97314%22%2C%22e%22%3A1443451598232%7D; __utmt=1; _ga=GA1.2.1544596104.1442846674; _dc_gtm_UA-12908513-11=1; previouslyViewedListings=15916963~15530373~16054760~; cookieTest=val; __utma=134851954.1544596104.1442846674.1442947958.1442949910.7; __utmb=134851954.3.9.1442949912898; __utmc=134851954; __utmz=134851954.1442846674.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); fsr.s=%7B%22cp%22%3A%7B%22province%22%3A%22qc%22%2C%22Language%22%3A%22Culture%3Den-CA%22%7D%2C%22v2%22%3A-2%2C%22v1%22%3A1%2C%22rid%22%3A%22d464cf5-82675489-babf-9372-97314%22%2C%22to%22%3A5%2C%22c%22%3A%22http%3A%2F%2Fwww.realtor.ca%2FResidential%2FSingle-Family%2F16054760%2F1641-Boul-Sunnybrooke-Dollard-Des-Ormeaux-Quebec-H9B1R4-East%22%2C%22pv%22%3A12%2C%22lc%22%3A%7B%22d0%22%3A%7B%22v%22%3A12%2C%22s%22%3Atrue%7D%7D%2C%22cd%22%3A0%2C%22f%22%3A1442950095993%2C%22sd%22%3A0%2C%22l%22%3A%22en%22%2C%22i%22%3A-1%7D");

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