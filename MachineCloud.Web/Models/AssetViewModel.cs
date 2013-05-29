using System.Collections.Generic;
using MachineCloud.Domain;

namespace MachineCloud.Web.Models
{
    public class AssetViewModel
    {
        public Asset Asset { get; set; }
        public Dictionary<AssetPropertyViewModel, AssetPropertyValue> PropertyValues { get; private set; }

        public AssetViewModel()
        {
            PropertyValues = new Dictionary<AssetPropertyViewModel, AssetPropertyValue>();
        }

        public void SetPropertyValue(AssetPropertyViewModel property, AssetPropertyValue propertyValue)
        {
            PropertyValues[property] = propertyValue;
        }

    }
}