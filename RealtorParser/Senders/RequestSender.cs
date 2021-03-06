﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace RealtorParser
{
    public class RequestSender
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
                       "&PropertyTypeId=300&TransactionTypeId=2&SortOrder=A&SortBy=1" +
                       "&LongitudeMin=-73.88823656811334&LongitudeMax=-73.75236658825494" +
                       "&LatitudeMin=45.450285497260325&LatitudeMax=45.50090157912774" +
                       "&PriceMin=200000&PriceMax=400000" +
                       "&BuildingTypeId=1" +
                       "&BedRange=3-0&BathRange=0-0&ParkingSpaceRange=0-0" +
                       "&viewState=m&Longitude=-73.71504589999999&Latitude=45.4645503" +
                       "&ZoomLevel=11" +
                       "&CurrentPage=1";

            if (Request_www_realtor_ca(body, out response))
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.realtor.ca/api/Listing.svc/PropertySearch_Post");

                request.KeepAlive = true;
                request.Accept = "*/*";
                request.Headers.Add("Origin", @"http://www.realtor.ca");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Headers.Add("DNT", @"1");
                request.Referer = "http://www.realtor.ca/map.aspx";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8,ru;q=0.6,ro;q=0.4,fr;q=0.2");
                request.Headers.Set(HttpRequestHeader.Cookie, @"GUID=a5d4c09e-aa25-4846-a54c-e61f77f97006; Language=Culture=en-CA; TargetPage=/map.aspx#CultureID=1&Latitude=45.4645503&Longitude=-73.71504589999999&ZoomLevel=11&PropertyTypeId=300&TransactionTypeId=2&PriceMin=75000&PriceMax=175000&BedRange=3-0&BathRange=0-0; TermsOfUseAgreement=ACCEPTED; ASP.NET_SessionId=kkpwq3hklubwmdfgv3oljhnc; fsr.r=%7B%22d%22%3A90%2C%22i%22%3A%22d464cf5-82675489-babf-9372-97314%22%2C%22e%22%3A1443451598232%7D; previouslyViewedListings=15916963~; __utmt=1; _dc_gtm_UA-12908513-11=1; _gali=button_realtor_go; __utma=134851954.1544596104.1442846674.1442850009.1442858913.3; __utmb=134851954.4.8.1442858927927; __utmc=134851954; __utmz=134851954.1442846674.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); cookieTest=val; fsr.s=%7B%22cp%22%3A%7B%22province%22%3A%22qc%22%2C%22Language%22%3A%22Culture%3Den-CA%22%7D%2C%22v2%22%3A-2%2C%22v1%22%3A1%2C%22rid%22%3A%22d464cf5-82675489-babf-9372-97314%22%2C%22to%22%3A4.6%2C%22c%22%3A%22http%3A%2F%2Fwww.realtor.ca%2Fmap.aspx%23CultureID%3D1%26Latitude%3D45.4645503%26Longitude%3D-73.71504589999999%26ZoomLevel%3D11%26PropertyTypeId%3D300%26TransactionTypeId%3D2%26PriceMin%3D75000%26PriceMax%3D150000%26BedRange%3D3-0%26BathRange%3D0-0%22%2C%22pv%22%3A9%2C%22lc%22%3A%7B%22d0%22%3A%7B%22v%22%3A9%2C%22s%22%3Atrue%7D%7D%2C%22cd%22%3A0%2C%22f%22%3A1442858928151%2C%22sd%22%3A0%2C%22l%22%3A%22en%22%2C%22i%22%3A-1%7D; _ga=GA1.2.1544596104.1442846674");

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