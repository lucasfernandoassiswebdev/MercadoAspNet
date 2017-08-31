using MercadoAplicacao.LoginApp;
using MercadoAplicacao.UsuarioApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Usuarios
{
    public class LoginController : AuthController
    {
        private readonly ILoginAplicacao _appLogin;
        private readonly IUsuarioAplicacao _appUsuario;

        public  LoginController(ILoginAplicacao login, IUsuarioAplicacao usuario)
        {
            _appLogin = login;
            _appUsuario = usuario;
        }

        public ActionResult Index()
        {
            var listaDeLogins = _appLogin.ListarTodos();
            return View(listaDeLogins);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Usuarios = _appUsuario.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Login login)
        {
            if(ModelState.IsValid)
            {
                _appLogin.Salvar(login);
                return RedirectToAction("Index"); 
            }
            return View(login);
        }

        public ActionResult Editar(int id)
        {
            var login = _appLogin.ListarPorId(id);
            if (login == null)
                return HttpNotFound();

            @ViewBag.Login = _appLogin.ListarPorId(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Login login)
        {
            if(ModelState.IsValid)
            {
                _appLogin.Salvar(login);
                return RedirectToAction("Index");
            }
            return View(login);
        }

        public ActionResult Excluir(int id)
        {
            var login = _appLogin.ListarPorId(id);
            if(login == null)
                return HttpNotFound();

            return View(login);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var login = _appLogin.ListarPorId(id);
            _appLogin.Excluir(login);
            return RedirectToAction("Index");
        }
    }
}