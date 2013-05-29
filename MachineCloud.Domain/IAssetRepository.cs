using System.Collections.Generic;

namespace MachineCloud.Domain
{
    public interface IAssetRepository
    {
        IEnumerable<Asset> GetAll(AssetType type);
        void Add(Asset asset);
        void Remove(Asset asset);
        Asset Find(AssetType type, string key);
    }
}
