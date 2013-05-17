using System.Collections.Generic;

namespace MachineCloud.Domain
{
    public class AssetPropertyValue
    {
        public AssetProperty Property { get; private set; }
        public Asset Asset { get; private set; }
        public string SystemValue { get; set; }
        public List<string> SystemValues { get; set; }
        public Asset AssetValue { get; set; }
        public List<Asset> AssetValues { get; set; }

        public AssetPropertyValue(Asset asset, AssetProperty property)
        {
            Asset = asset;
            Property = property;

            if (property.Type != null)
            {
                if (property.IsCollection)
                {
                    AssetValues = new List<Asset>();
                }
                else
                {
                    AssetValue = new Asset(property.Type);
                }
            }
        }
        
    }
}
