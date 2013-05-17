using System.Collections.Generic;
using System.Linq;

namespace MachineCloud.Domain
{
    public class Asset
    {
        public AssetType Type { get; private set; }
        public List<AssetPropertyValue> PropertyValues { get; private set; }

        public Asset(AssetType type)
        {
            Type = type;
            PropertyValues = new List<AssetPropertyValue>();

            foreach (var assetProperty in type.Properties)
            {
                PropertyValues.Add(new AssetPropertyValue(this, assetProperty));
            }
        }

        public AssetPropertyValue GetPropertyValue(string propertyName)
        {
            return PropertyValues.SingleOrDefault(x => x.Property.Name == propertyName);
        }
    }
}
