using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace R3.Service
{
    public class GenericDetails
    {
        public string MakeRequests(string url)
        {
            HttpWebResponse response;
            string responseText = null;

            if (Request_www_remax_quebec_com(url, out response))
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

        private bool Request_www_remax_quebec_com(string url, out HttpWebResponse response)
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
                request.Referer = "http://passerelle.centris.ca/Redirect2.aspx?CodeDest=REMAX&NoMls=MT20541479&Source=&Langue=E&autord=1";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8,ru;q=0.6,ro;q=0.4,fr;q=0.2");
                //request.Headers.Set(HttpRequestHeader.Cookie, @"JSESSIONID=5ADD6A6FCF7091DE4E7ED9FF64CEE731.sc2; __atuvc=4%7C38; __atuvs=5604065f3465f682000; __utmt=1; __utma=32288829.1549894873.1442954173.1443021077.1443104352.4; __utmb=32288829.1.10.1443104352; __utmc=32288829; __utmz=32288829.1443021077.3.2.utmcsr=passerelle.centris.ca|utmccn=(referral)|utmcmd=referral|utmcct=/Redirect2.aspx");

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