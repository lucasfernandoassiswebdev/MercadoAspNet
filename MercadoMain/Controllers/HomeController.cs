using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProjetoMercado.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(Login login)
        //{

        //}
    }
}