using System.Web.Mvc;

namespace MachineCloud.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
