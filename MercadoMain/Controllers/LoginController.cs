using Mercado.Aplicacao.UsuarioApp;
using MercadoAplicacao.LoginApp;
using MercadoDominio.Entidades;
using System.Web.Mvc;

namespace MercadoMain.Controllers
{
    public class LoginController : Controller
    {
        private LoginAplicacao appLogin;
        private UsuarioAplicacao appUsuario;

        public  LoginController()
        {
            appLogin = LoginAplicacaoConstrutor.LoginAplicacaoADO();
            appUsuario = UsuarioAplicacaoConstrutor.UsuarioAplicacaoADO();
        }

        public ActionResult Index()
        {
            var listaDeLogins = appLogin.ListarTodos();
            return View(listaDeLogins);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Usuarios = appUsuario.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Login login)
        {
            if(ModelState.IsValid)
            {
                appLogin.Salvar(login);
                return RedirectToAction("Index"); 
            }
            return View(login);
        }

        public ActionResult Editar(int id)
        {
            var login = appLogin.ListarPorId(id);
            if (login == null)
                return HttpNotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Login login)
        {
            if(ModelState.IsValid)
            {
                appLogin.Salvar(login);
                return RedirectToAction("Index");
            }
            return View(login);
        }

        public ActionResult Excluir(int id)
        {
            var login = appLogin.ListarPorId(id);
            if(login == null)
                return HttpNotFound();

            return View(login);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Login login)
        {
            appLogin.Excluir(login);
            return RedirectToAction("Index");
        }
    }
}