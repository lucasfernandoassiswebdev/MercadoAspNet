using System.Web.Mvc;

namespace MercadoMain.Controllers
{
    public class AuthController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Login"] == null)
                filterContext.Result = new RedirectResult("/Home/Unauthorized");

            base.OnActionExecuting(filterContext);
        }
    }
}
