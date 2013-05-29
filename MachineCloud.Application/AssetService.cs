using MachineCloud.Domain;
using Ninject;
using System.Collections.Generic;

namespace MachineCloud.Application
{
    public class AssetService : IAssetService
    {
        [Inject]
        public IAssetRepository AssetRepository { get; set; }

        public IEnumerable<Asset> GetAllAssetsByAssetType(AssetType type)
        {
            return AssetRepository.GetAll(type);
        }

        public void AddAsset(Asset asset)
        {
            AssetRepository.Add(asset);
        }

        public void RemoveAsset(Asset asset)
        {
            AssetRepository.Remove(asset);
        }

        public Asset FindAsset(AssetType assetType, string uniqueIdentifier)
        {
            return AssetRepository.Find(assetType, uniqueIdentifier);
        }
    }
}
