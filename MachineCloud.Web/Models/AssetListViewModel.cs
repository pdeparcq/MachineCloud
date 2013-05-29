using System.Collections.Generic;
using System.Linq;
using MachineCloud.Domain;

namespace MachineCloud.Web.Models
{
    public class AssetListViewModel
    {
        public AssetType AssetType { get; set; }
        public List<AssetPropertyViewModel> Properties { get; private set; }
        public List<AssetViewModel> Assets { get; private set; }

        public AssetListViewModel()
        {
            Properties = new List<AssetPropertyViewModel>();
            Assets = new List<AssetViewModel>();
        }

        public void AddProperty(AssetPropertyViewModel property)
        {
            Properties.Add(property);
        }

        public void AddAsset(AssetViewModel asset)
        {
            Assets.Add(asset);
        }
    }
}