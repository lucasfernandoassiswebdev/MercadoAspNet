using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}