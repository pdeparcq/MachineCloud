using System.Linq;
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
            ViewBag.AssetTypes = AssetTypeService.GetAllAssetTypes().Where(x => x.HasUniqueIdentifier);
            base.Initialize(requestContext);
        }

    }
}
