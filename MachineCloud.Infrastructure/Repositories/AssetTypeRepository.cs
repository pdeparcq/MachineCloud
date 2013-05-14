using System.Data.Entity;
using System.Linq;
using MachineCloud.Domain;
namespace MachineCloud.Infrastructure.Repositories
{
    public class AssetTypeRepository : RepositoryBase<MachineCloudContext, AssetType>, IAssetTypeRepository
    {
        protected override System.Data.Entity.IDbSet<AssetType> DbSet
        {
            get { return Context.AssetTypes; }
        }

        public override AssetType Find(params object[] keys)
        {
            var name = (string) keys[0];
            return DbSet.Include(x => x.Properties).Single(x => x.Name == name);
        }

        public override System.Collections.Generic.IEnumerable<AssetType> GetAll()
        {
            return DbSet
                .Include(x => x.Properties.Select(p => p.UnitOfMeasurement))
                .Include(x => x.Properties.Select(p => p.Type))
                .ToList();
        }

        public override void Remove(AssetType entity)
        {
            foreach (var property in Context.AssetProperties.Where(x => x.ParentAssetTypeName == entity.Name))
            {
                Context.AssetProperties.Remove(property);
            }
            base.Remove(entity);
        }

        public void RemoveProperty(string name, string propertyName)
        {
            Context.AssetProperties.Remove(Find(name).Properties.Single(x => x.Name == propertyName));
        }
    }
}
