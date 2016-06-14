using System;

namespace R3.DataStorage.Tables
{
    public class RealEstate
    {
        public int Id { get; set; }
        public DateTime DateTaken { get; set; }
        public string MlsId { get; set; }
        public string MlsNumber { get; set; }
        public string Price { get; set; }
        public string YearBuild { get; set; }
        public string NeighbourhoodName { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Type { get; set; }
        public string ParkingType { get; set; }
        public string AddressText { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PublicRemarks { get; set; }
        public string RelativeDetailsURL { get; set; }
        public string AlternateURL { get; set; }
        public int PriceCoefficient { get; set; }
        public string Individuals { get; set; }
        public string Status { get; set; }

        //public virtual ICollection<RealEstateHistory> RealEstateHistories { get; set; }
    }
}
