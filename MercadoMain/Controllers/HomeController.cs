using MercadoAplicacao.LoginApp;
using MercadoAplicacao.UsuarioApp;
using MercadoDominio.Entidades;
using System.Web.Mvc;

namespace MercadoMain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginAplicacao _appLogin;
        private readonly IUsuarioAplicacao _appUsuario;

        public HomeController(ILoginAplicacao login, IUsuarioAplicacao usuario)
        {
            _appLogin = login;
            _appUsuario = usuario;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            ModelState.AddModelError("LOGIN", "Você não está logado ou suas informações de login estão incorretas!");
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                //verificando se o login e senha batem
                var autorizacao = _appLogin.VerificaLogin(login);

                if (autorizacao != 0)
                {
                    //se baterem é verificado o nível do usuário e atribuído a session
                    var nivel = _appUsuario.VerificaNivelUsuario(autorizacao);
                    
                    Session["Login"] = nivel;
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("LOGIN", "Você não está logado ou suas informações de login estão incorretas!");
                return View("Index");
            }

            return View(login);
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