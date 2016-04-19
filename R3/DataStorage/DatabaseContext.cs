using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using R3.DataStorage.Tables;

namespace R3.DataStorage
{
    public class MainStorage : DbContext
    {
        public MainStorage() : base("MainStorage")
        {
        }

        public DbSet<AccessLog> AccessLogs { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<RealEstateHistory> RealEstateHistories { get; set; }
        public DbSet<RealEstateSold> SoldRealEstates { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccessLogMap());
            modelBuilder.Configurations.Add(new RealEstateMap());
            modelBuilder.Configurations.Add(new RealEstateHistoryMap());
            modelBuilder.Configurations.Add(new RealEstateSoldMap());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}