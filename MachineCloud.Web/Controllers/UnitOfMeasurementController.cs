using System.Collections.Generic;
using System.Linq;
using MachineCloud.Domain;
using System;
using System.Web.Mvc;
using MachineCloud.Web.Models;

namespace MachineCloud.Web.Controllers
{
    public class UnitOfMeasurementController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(new UnitOfMeasurementViewModel
                {
                    UnitsOfMeasurement = UnitOfMeasurementService.GetAllUnitsOfMeasurement(),
                    SelectableSystemValueTypes = new List<SystemTypes>((SystemTypes[])Enum.GetValues(typeof(SystemTypes)))
                                                                .Select(x => new SelectListItem
                                                                {
                                                                    Text = Enum.GetName(typeof(SystemTypes), x),
                                                                    Value = Enum.GetName(typeof(SystemTypes), x)
                                                                }).ToList(),
                });
        }

        public ActionResult Create(string name, string shortName, string systemValueType)
        {
            UnitOfMeasurementService.CreateNewUnitOfMeasurement(name, shortName,
                                                                (SystemTypes)
                                                                Enum.Parse(typeof (SystemTypes), systemValueType));
            return RedirectToAction("Index");
        }

        public ActionResult Remove(string name)
        {
            UnitOfMeasurementService.RemoveUnitOfMeasurement(name);
            return RedirectToAction("Index");
        }
    }
}
