using System.Collections.Generic;
using System.Web.Mvc;
using MachineCloud.Domain;

namespace MachineCloud.Web.Models
{
    public class AssetPropertyViewModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public AssetProperty Property { get; set; }
        public IEnumerable<SelectListItem> PossibleValues { get; set; }
    }
}