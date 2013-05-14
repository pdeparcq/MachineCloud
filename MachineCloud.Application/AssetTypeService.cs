using System.Collections.Generic;
using MachineCloud.Domain;
using Ninject;

namespace MachineCloud.Application
{
    public class AssetTypeService : IAssetTypeService
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        [Inject]
        public IAssetTypeRepository AssetTypeRepository { get; set; }

        public IEnumerable<AssetType> GetAllAssetTypes()
        {
            return AssetTypeRepository.GetAll();
        }

        public AssetType CreateNewAssetType(string assetTypeName)
        {
            AssetTypeRepository.Add(new AssetType { Name = assetTypeName });
            UnitOfWork.Commit();
            return AssetTypeRepository.Find(assetTypeName);
        }

        public void RemoveAssetType(string assetTypeName)
        {
            AssetTypeRepository.Remove(AssetTypeRepository.Find(assetTypeName ?? ""));
            UnitOfWork.Commit();
        }

        public void AddPropertyToAssetType(string assetTypeName, AssetProperty property)
        {
            var assetType = AssetTypeRepository.Find(assetTypeName);
            assetType.AddProperty(property);
            AssetTypeRepository.Update(assetType);
            UnitOfWork.Commit();
        }

        public void RemovePropertyFromAssetType(string assetTypeName, string propertyName)
        {
            AssetTypeRepository.RemoveProperty(assetTypeName, propertyName);
            UnitOfWork.Commit();
        }

        public AssetType FindAssetTypeByName(string assetTypeName)
        {
            return AssetTypeRepository.Find(assetTypeName);
        }
    }
}
