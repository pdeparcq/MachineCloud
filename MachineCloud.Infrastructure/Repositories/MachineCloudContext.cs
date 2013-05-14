using System.Data.Entity.ModelConfiguration.Conventions;
using MachineCloud.Domain;
using System.Data.Entity;
namespace MachineCloud.Infrastructure.Repositories
{
    public class MachineCloudContext : DbContext, IUnitOfWork
    {
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AssetProperty> AssetProperties { get; set; }
        public DbSet<UnitOfMeasurement> UnitsOfMeasurement { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
