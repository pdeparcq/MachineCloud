using MachineCloud.Domain;
namespace MachineCloud.Infrastructure.Repositories
{
    public class UnitOfMeasurementRepository : RepositoryBase<MachineCloudContext, UnitOfMeasurement>, IUnitOfMeasurementRepository
    {
        protected override System.Data.Entity.IDbSet<UnitOfMeasurement> DbSet
        {
            get { return Context.UnitsOfMeasurement; }
        }
    }
}
