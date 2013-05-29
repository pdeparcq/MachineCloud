using System;
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

        private string _uniqueIdentifier;

        public string UniqueIdentifier
        {
            get {
                return _uniqueIdentifier ??
                       (_uniqueIdentifier =
                        Type.HasUniqueIdentifier
                            ? GetPropertyValue(Type.UniqueIdentifier.Name).SystemValue
                            : Guid.NewGuid().ToString());
            }
            set { _uniqueIdentifier = value; }
        }

        public void SetPropertySystemValue(string propertyName, string value)
        {
            GetPropertyValue(propertyName).SystemValue = value;
        }

        public AssetPropertyValue GetPropertyValue(string propertyName)
        {
            if (propertyName.IndexOf(".", StringComparison.Ordinal) > 0)
            {
                return
                    PropertyValues.SingleOrDefault(x => x.Property.Name == propertyName.Substring(0, propertyName.IndexOf(".", System.StringComparison.Ordinal)))
                                  .AssetValue.GetPropertyValue(propertyName.Substring(propertyName.IndexOf(".", System.StringComparison.Ordinal)+1));
            }
            return PropertyValues.SingleOrDefault(x => x.Property.Name == propertyName);
        }
    }
}
