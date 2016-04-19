﻿using System;

namespace XMLCleanup
{
    public class RealEstateSold
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
        public string PublicRemarks { get; set; }
        public string RelativeDetailsURL { get; set; }
        public string AlternateURL { get; set; }
        public int PriceCoefficient { get; set; }
        public string Individuals { get; set; }
        public string Status { get; set; }
    }
}