using System.Collections.Generic;
using MachineCloud.Domain;

namespace MachineCloud.Application
{
    public interface IAssetService
    {
        IEnumerable<Asset> GetAllAssetsByAssetType(AssetType type);
        void AddAsset(Asset asset);
        void RemoveAsset(Asset asset);
        Asset FindAsset(AssetType assetType, string uniqueIdentifier);
    }
}
