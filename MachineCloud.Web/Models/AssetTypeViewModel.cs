using System.Collections.Generic;
using System.Web.Mvc;
using MachineCloud.Domain;

namespace MachineCloud.Web.Models
{
    public class AssetTypeViewModel
    {
        public IEnumerable<AssetType> AssetTypes { get; set; }
        public IEnumerable<SelectListItem> SelectableSystemValueTypes { get; set; }
        public IEnumerable<SelectListItem> SelectableUnitsOfMeasurement { get; set; }
        public IEnumerable<SelectListItem> SelectableAssetTypes { get; set; }
    }
}