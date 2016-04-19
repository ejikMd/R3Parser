using System.Data.Entity.ModelConfiguration;

namespace R3.DataStorage.Tables
{
    public class AccessLogMap : EntityTypeConfiguration<AccessLog>
    {
        public AccessLogMap()
        {
            ToTable("AccessLogs", "dbo");

            HasKey(k => k.Id);

            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.AccessDate).HasColumnName("AccessDate");
            Property(p => p.Ip).HasColumnName("Ip");
        }
    }
}