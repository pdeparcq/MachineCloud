using MachineCloud.Domain;
using System.Collections.Generic;

namespace MachineCloud.Application
{
    public interface IAssetTypeService
    {
        IEnumerable<AssetType> GetAllAssetTypes();
        AssetType FindAssetTypeByName(string assetTypeName);
        AssetType CreateNewAssetType(string assetTypeName);
        void RemoveAssetType(string assetTypeName);
        void AddPropertyToAssetType(string assetTypeName, AssetProperty property);
        void RemovePropertyFromAssetType(string assetTypeName, string propertyName);
    }
}
