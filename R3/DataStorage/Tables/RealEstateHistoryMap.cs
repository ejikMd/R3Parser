using System.Data.Entity.ModelConfiguration;

namespace R3.DataStorage.Tables
{
    public class RealEstateHistoryMap : EntityTypeConfiguration<RealEstateHistory>
    {
        public RealEstateHistoryMap()
        {
            ToTable("RealEstateHistory", "dbo");

            HasKey(u => new { u.MlsNumber, u.DateTaken });

            Property(p => p.DateTaken).HasColumnName("dateTaken");
            Property(p => p.MlsNumber).HasColumnName("mlsNumber");
            Property(p => p.Status).HasColumnName("Status");
        }
    }
}