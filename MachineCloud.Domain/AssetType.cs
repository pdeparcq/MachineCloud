using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineCloud.Domain
{
    public class AssetType
    {
        [Key]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("ParentAssetTypeName")]
        public ICollection<AssetProperty> Properties { get; set; }

        public AssetType()
        {
            Properties = new List<AssetProperty>();
        }

        public void AddProperty(AssetProperty property)
        {
            Properties.Add(property);
        }
    }
}
