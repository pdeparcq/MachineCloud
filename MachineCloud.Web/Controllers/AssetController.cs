using System.Web.Mvc;
using MachineCloud.Web.Models;

namespace MachineCloud.Web.Controllers
{
    public class AssetController : ControllerBase
    {
        public ActionResult Index(string assetType)
        {
            return View(new AssetViewModel{AssetType = AssetTypeService.FindAssetTypeByName(assetType)});
        }

        public ActionResult Create(string assetType)
        {
            return RedirectToAction("Index");
        }
    }
}
