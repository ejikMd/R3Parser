using System;

namespace R3.Models
{
    public class RealEstateViewModel
    {
        public int Id { get; set; }
        public DateTime SoldDate { get; set; }
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
        public string PublicRemarks { get; set; }
        public string RelativeDetailsURL { get; set; }
        public string AlternateURL { get; set; }
        public int PriceCoefficient { get; set; }
        public string Status { get; set; }

        public bool IsNew { get; set; }
    }
}
