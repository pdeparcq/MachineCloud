using System.Web.Mvc;
using MachineCloud.Application;
using Ninject;

namespace MachineCloud.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        [Inject]
        public IAssetTypeService AssetTypeService { get; set; }

        [Inject]
        public IUnitOfMeasurementService UnitOfMeasurementService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            int a = 0;
            ViewBag.AssetTypes = AssetTypeService.GetAllAssetTypes();
            base.Initialize(requestContext);
        }

    }
}
