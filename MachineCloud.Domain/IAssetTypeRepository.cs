namespace MachineCloud.Domain
{
    public interface IAssetTypeRepository : IRepository<AssetType>
    {
        void RemoveProperty(string name, string propertyName);
    }
}
