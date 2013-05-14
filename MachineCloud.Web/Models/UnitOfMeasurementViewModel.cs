using MachineCloud.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MachineCloud.Web.Models
{
    public class UnitOfMeasurementViewModel
    {
        public IEnumerable<UnitOfMeasurement> UnitsOfMeasurement { get; set; }
        public IEnumerable<SelectListItem> SelectableSystemValueTypes { get; set; }
    }
}