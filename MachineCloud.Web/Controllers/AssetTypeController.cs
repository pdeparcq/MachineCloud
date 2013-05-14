using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MachineCloud.Domain;
using MachineCloud.Web.Models;

namespace MachineCloud.Web.Controllers
{
    public class AssetTypeController : ControllerBase    {
     
        public ActionResult Index()
        {
            var assetTypes = AssetTypeService.GetAllAssetTypes();
            return View(new AssetTypeViewModel
                {
                    AssetTypes = assetTypes,
                    SelectableSystemValueTypes = new List<SystemTypes>((SystemTypes[])Enum.GetValues(typeof(SystemTypes)))
                                                                .Select(x => new SelectListItem
                                                                    {
                                                                        Text = Enum.GetName(typeof(SystemTypes), x), 
                                                                        Value = Enum.GetName(typeof(SystemTypes), x)
                                                                    }).ToList(),
                    SelectableUnitsOfMeasurement = UnitOfMeasurementService.GetAllUnitsOfMeasurement()
                                                                .Select(x => new SelectListItem
                                                                    {
                                                                        Text = string.Format("{0} ({1})", x.Name, x.ShortName),
                                                                        Value = x.Name
                                                                    }).ToList(),
                    SelectableAssetTypes = assetTypes.Select(x => new SelectListItem{Text = x.Name, Value = x.Name}).ToList()
                });
        }

        public ActionResult Create(string name)
        {
            AssetTypeService.CreateNewAssetType(name);
            return RedirectToAction("Index");
        }

        public ActionResult AddProperty(string name, string propertyName, string optionsPropertyType, string selectedSystemValueType, string selectedUnitOfMeasurement, string selectedAssetType)
        {
            
            var property = new AssetProperty {Name = propertyName};
            if (optionsPropertyType == "optionSystemValueType")
            {
                property.ValueType = (SystemTypes) Enum.Parse(typeof (SystemTypes), selectedSystemValueType);
            }
            else if (optionsPropertyType == "optionUnitOfMeasurement")
            {
                property.UnitOfMeasurement = UnitOfMeasurementService.FindUnitOfMeasurementByName(selectedUnitOfMeasurement);
            }
            else if (optionsPropertyType == "optionAssetType")
            {
                property.Type = AssetTypeService.FindAssetTypeByName(selectedAssetType);
            }
            AssetTypeService.AddPropertyToAssetType(name, property);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(string name)
        {
            AssetTypeService.RemoveAssetType(name);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveProperty(string name, string propertyName)
        {
            AssetTypeService.RemovePropertyFromAssetType(name, propertyName);
            return RedirectToAction("Index");
        }
    }
}
