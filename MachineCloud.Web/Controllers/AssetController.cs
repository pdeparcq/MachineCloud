using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MachineCloud.Domain;
using MachineCloud.Web.Models;
using Ninject;
using MachineCloud.Application;
using System.Text;

namespace MachineCloud.Web.Controllers
{
    public class AssetController : ControllerBase
    {
        [Inject]
        public IAssetService AssetService { get; set; }

        public ActionResult Index(string assetType)
        {
            return View(CreateAssetListViewModel(assetType, true));
        }

        private AssetListViewModel CreateAssetListViewModel(string type, bool includeAssets)
        {
            var viewModel = new AssetListViewModel() { AssetType = AssetTypeService.FindAssetTypeByName(type) };

            AddPropertyViewModels(viewModel, viewModel.AssetType, null);
            
            if (includeAssets)
            {
                foreach (var asset in AssetService.GetAllAssetsByAssetType(viewModel.AssetType))
                {
                    AddAssetViewModel(viewModel, asset);
                }    
            }

            return viewModel;
        }

        private void AddAssetViewModel(AssetListViewModel viewModel, Asset asset)
        {
            var assetViewModel = new AssetViewModel(){Asset = asset};
            foreach (var property in viewModel.Properties)
            {
                assetViewModel.SetPropertyValue(property, asset.GetPropertyValue(property.FullName));
            }
            viewModel.AddAsset(assetViewModel);
        }

        private void AddPropertyViewModels(AssetListViewModel viewModel, AssetType assetType, string parentName)
        {
            foreach (var property in assetType.Properties)
            {
                var fullName = parentName == null ? property.Name : new StringBuilder(parentName).Append(".").Append(property.Name).ToString();
                if (property.ValueType != null || property.UnitOfMeasurement != null)
                    AddPropertyToViewModel(viewModel, property, fullName, null);
                if (property.Type != null)
                {
                    if (!property.Type.HasUniqueIdentifier)
                    {
                        AddPropertyViewModels(viewModel, property.Type, fullName);
                    }
                    else
                    {
                        var possibleValues = AssetService.GetAllAssetsByAssetType(property.Type).Select(x => new SelectListItem()
                            {
                                Value = x.UniqueIdentifier,
                                Text = x.UniqueIdentifier
                            });
                        AddPropertyToViewModel(viewModel, property, fullName, possibleValues);
                    }
                }
            }
        }

        private static void AddPropertyToViewModel(AssetListViewModel viewModel, AssetProperty property, string fullName, IEnumerable<SelectListItem> possibleValues)
        {
            viewModel.AddProperty(new AssetPropertyViewModel()
                {
                    Name = property.Name,
                    FullName = fullName,
                    Property = property,
                    PossibleValues = possibleValues
                });
        }

        public ActionResult Create(string assetType, FormCollection values)
        {
            var viewModel = CreateAssetListViewModel(assetType, false);
            var asset = new Asset(viewModel.AssetType);
            foreach (var property in viewModel.Properties)
            {
                asset.GetPropertyValue(property.FullName).SystemValue = values[property.FullName];
            }
            AssetService.AddAsset(asset);
            return RedirectToAction("Index", new{assetType});
        }

        public ActionResult Remove(string assetType, string uniqueIdentifier)
        {
            AssetService.RemoveAsset(AssetService.FindAsset(AssetTypeService.FindAssetTypeByName(assetType), uniqueIdentifier));
            return RedirectToAction("Index", new { assetType });
        }
    }
}
