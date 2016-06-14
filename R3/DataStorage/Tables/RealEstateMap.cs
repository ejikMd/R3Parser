using System.Data.Entity.ModelConfiguration;

namespace R3.DataStorage.Tables
{
    public class RealEstateMap : EntityTypeConfiguration<RealEstate>
    {
        public RealEstateMap()
        {
            ToTable("RealEstate", "dbo");

            HasKey(k => k.Id);

            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.DateTaken).HasColumnName("dateTaken");
            Property(p => p.MlsId).HasColumnName("MlsId");
            Property(p => p.MlsNumber).HasColumnName("MlsNumber");
            Property(p => p.Price).HasColumnName("Price");
            Property(p => p.YearBuild).HasColumnName("YearBuild");
            Property(p => p.NeighbourhoodName).HasColumnName("NeighbourhoodName");
            Property(p => p.Bedrooms).HasColumnName("Bedrooms");
            Property(p => p.Bathrooms).HasColumnName("Bathrooms");
            Property(p => p.Type).HasColumnName("Type");
            Property(p => p.ParkingType).HasColumnName("ParkingType");
            Property(p => p.AddressText).HasColumnName("AddressText");
            Property(p => p.Latitude).HasColumnName("Latitude");
            Property(p => p.Longitude).HasColumnName("Longitude");
            Property(p => p.PublicRemarks).HasColumnName("PublicRemarks");
            Property(p => p.RelativeDetailsURL).HasColumnName("RelativeDetailsURL");
            Property(p => p.AlternateURL).HasColumnName("AlternateURL");
            Property(p => p.PriceCoefficient).HasColumnName("PriceCoefficient");
            Property(p => p.Individuals).HasColumnName("Individuals");
            Property(p => p.Status).HasColumnName("Status");
        }
    }
}