using MercadoAplicacao.LoginApp;
using MercadoDominio.Entidades;
using System.Web.Mvc;

namespace MercadoMain.Controllers
{
    public class HomeController : Controller
    {
        private ILoginAplicacao _appLogin; 

        public HomeController(ILoginAplicacao login)
        {
            _appLogin = login;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            ModelState.AddModelError("LOGIN", "Você não está logado!");
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                var logins = _appLogin.ListarTodos();

                foreach (var log in logins)
                {
                    if (log.LoginU == login.LoginU && log.Senha == login.Senha)
                    {
                        Session["Login"] = log;
                    }
                }
                return RedirectToAction("LogadoComSucesso");
            }
            return View(login);
        }

        public ActionResult LogadoComSucesso()
        {
            if (Session["Login"] == null)
            {
                ViewBag.Resultado = "login ou senha incorretos";
            }
            else
            {
                ViewBag.Resultado = ((Login)Session["Login"]).Funcionario.Nome + " logado com sucesso";
            }

            return View();
        }

        public ActionResult Logoff()
        {
            if (Session["Login"] != null)
            {
                Session.Clear();
            }

            return RedirectToAction("Index","Home");
        }
    }
}