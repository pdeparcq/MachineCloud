using System;
using System.Collections.Generic;
using System.Linq;
using MachineCloud.Domain;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;
using Ninject;

namespace MachineCloud.Infrastructure.Repositories
{
    public class AssetEntity : TableEntity
    {
        public AssetEntity(Asset asset)
        {
            this.PartitionKey = asset.Type.Name;
            this.RowKey = asset.UniqueIdentifier;
        }

        public AssetEntity(){}

        public string Id
        {
            get { return this.RowKey; }
        }

        public string AssetTypeName
        {
            get { return this.PartitionKey; }
        }
    }

    public class AssetPropertyValueEntity : TableEntity
    {
        public AssetPropertyValueEntity(string assetId, string assetPropertyName)
        {
            this.RowKey = assetId;
            this.PartitionKey = assetPropertyName;
        }

        public AssetPropertyValueEntity(){}

        public string AssetId
        {
            get { return this.RowKey; }
        }

        public string Name
        {
            get { return this.PartitionKey; }
        }

        public string SystemValue { get; set; }

        public string AssetValueId { get; set; }

    }

    public class AssetRepository : IAssetRepository
    {
        private readonly CloudTable _cloudAssetTable;
        private readonly CloudTable _cloudAssetPropertyValueTable;

        [Inject]
        public IAssetTypeRepository AssetTypeRepository { get; set; }

        public AssetRepository()
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            _cloudAssetTable = tableClient.GetTableReference("assets");
            _cloudAssetTable.CreateIfNotExists();

            _cloudAssetPropertyValueTable = tableClient.GetTableReference("assetpropertyvalues");
            _cloudAssetPropertyValueTable.CreateIfNotExists();
        }

        public IEnumerable<Asset> GetAll(AssetType type)
        {
            var query =
                new TableQuery<AssetEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                                                                                                    QueryComparisons
                                                                                                        .Equal,
                                                                                                    type.Name));
            return _cloudAssetTable.ExecuteQuery(query).Select(CreateAsset).ToList();
        }

        public void Add(Asset asset)
        {
            AddAsset(asset);
        }

        private string AddAsset(Asset asset)
        {
            var assetEntity = new AssetEntity(asset);
            var operation = TableOperation.Insert(assetEntity);
            _cloudAssetTable.Execute(operation);

            foreach (var propertyValue in asset.PropertyValues)
            {
                var assetPropertyValueEntity = new AssetPropertyValueEntity(assetEntity.Id, propertyValue.Property.Name);

                if (propertyValue.SystemValue != null)
                    assetPropertyValueEntity.SystemValue = propertyValue.SystemValue;

                if (propertyValue.AssetValue != null)
                {
                    assetPropertyValueEntity.AssetValueId = propertyValue.AssetValue.Type.HasUniqueIdentifier ? propertyValue.AssetValue.UniqueIdentifier : AddAsset(propertyValue.AssetValue);
                }

                operation = TableOperation.Insert(assetPropertyValueEntity);
                _cloudAssetPropertyValueTable.Execute(operation);

            }
            return assetEntity.Id;
        }

        public Asset Find(AssetType type, string key)
        {
           var assetEntity = Retrieve<AssetEntity>(_cloudAssetTable, type.Name, key);
           return assetEntity != null ? CreateAsset(assetEntity) : null;
        }

        private T Retrieve<T>(CloudTable table, string partitionKey, string rowKey) where T:class,ITableEntity
        {
            var operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var retrievedResult = table.Execute(operation);
            if (retrievedResult.Result != null)
            {
                return (T)retrievedResult.Result;
            }
            return null;
        }

        private Asset CreateAsset(AssetEntity assetEntity)
        {
            var asset = new Asset(AssetTypeRepository.Find(assetEntity.AssetTypeName));
            asset.UniqueIdentifier = assetEntity.Id;
            var query =
                new TableQuery<AssetPropertyValueEntity>().Where(TableQuery.GenerateFilterCondition("RowKey",
                                                                                                    QueryComparisons
                                                                                                        .Equal,
                                                                                                    assetEntity.Id));
            foreach (var propertyValueEntity in _cloudAssetPropertyValueTable.ExecuteQuery(query))
            {
                var propertyValue = asset.GetPropertyValue(propertyValueEntity.Name);

                if (propertyValueEntity.SystemValue != null)
                    propertyValue.SystemValue = propertyValueEntity.SystemValue;

                if (propertyValueEntity.AssetValueId != null)
                    propertyValue.AssetValue = Find(propertyValue.AssetValue.Type, propertyValueEntity.AssetValueId);

            }

            return asset;
        }
    
        public void Remove(Asset asset)
        {
            foreach (var propertyValue in asset.PropertyValues)
            {
                RemovePropertyValue(propertyValue);
            }
            var operation = TableOperation.Delete(Retrieve<AssetEntity>(_cloudAssetTable, asset.Type.Name, asset.UniqueIdentifier));
            _cloudAssetTable.Execute(operation);
        }

        private void RemovePropertyValue(AssetPropertyValue propertyValue)
        {
            if (propertyValue.AssetValue != null && !propertyValue.AssetValue.Type.HasUniqueIdentifier)
            {
                Remove(propertyValue.AssetValue);
            }
            var assetPropertyValueEntity = Retrieve<AssetPropertyValueEntity>(_cloudAssetPropertyValueTable, propertyValue.Property.Name,
                                                       propertyValue.Asset.UniqueIdentifier);
            if (assetPropertyValueEntity != null)
            {
                var operation = TableOperation.Delete(assetPropertyValueEntity);
                _cloudAssetPropertyValueTable.Execute(operation);    
            }
        }
    }
}
