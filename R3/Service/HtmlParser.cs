using HtmlAgilityPack;

namespace R3.Service
{
    public class HtmlParser
    {
        public HtmlDocument Parse(string htmlString)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            return doc;
        }

        public string GetYear(HtmlDocument doc)
        {
            if (doc.GetElementbyId("builtin_value") != null)
                return doc.GetElementbyId("builtin_value").InnerText;

            return "";
        }

        public string GetNeighbourhoodName(HtmlDocument doc)
        {
            if (doc.GetElementbyId("neighborhoodname_value") != null)
                return doc.GetElementbyId("neighborhoodname_value").InnerText;

            return "";
        }

        public string GetParkingType(HtmlDocument doc)
        {
            if (doc.GetElementbyId("parkingtype_value") != null)
                return doc.GetElementbyId("parkingtype_value").InnerText;

            return "";
        }
    }
}