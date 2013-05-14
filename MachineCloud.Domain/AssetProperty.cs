using System.ComponentModel.DataAnnotations.Schema;

namespace MachineCloud.Domain
{
    public class AssetProperty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ParentAssetTypeName { get; set; }

        public AssetType ParentAssetType { get; set; }

        public string Description { get; set; }

        public bool IsMandatory { get; set; }

        public bool IsUniqueIdentifier { get; set; }

        public bool IsCollection { get; set; }

        public SystemTypes? ValueType { get; set; }

        public string UnitOfMeasurementName { get; set; }

        [ForeignKey("UnitOfMeasurementName")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public string TypeAssetTypeName { get; set; }

        [ForeignKey("TypeAssetTypeName")]
        public AssetType Type { get; set; }
    }
}
