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
            ViewBag.Usuarios = _appUsuario.ListarUsuariosSemLogin();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Login login)
        {
            if(ModelState.IsValid)
            {
                if (login.Usuario == 0)
                {
                    ModelState.AddModelError("LOGIN", "Todos os funcionários já tem um login!");
                    ViewBag.Usuarios = _appUsuario.ListarUsuariosSemLogin();
                    return View("Cadastrar");
                }

                var equal = _appLogin.VerificaExistenciaSimilar(login);
                if (equal == 1)
                {
                    ModelState.AddModelError("LOGIN", "Este funcionário já tem um login ou já existe um login igual a este!");
                    ViewBag.Usuarios = _appUsuario.ListarUsuariosSemLogin();
                    return View("Cadastrar");
                }

                _appLogin.Salvar(login);
                return RedirectToAction("Index"); 
            }

            ViewBag.Usuarios = _appUsuario.ListarUsuariosSemLogin();
            return View(login);
        }

        public ActionResult Editar(int id)
        {
            var login = _appLogin.ListarPorId(id);
            if (login == null)
                return HttpNotFound();

            ViewBag.Login = _appLogin.ListarPorId(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Login login)
        {
            if(ModelState.IsValid)
            {
                var equal = _appLogin.VerificaExistenciaSimilar(login);
                if (equal == 1)
                {
                    ModelState.AddModelError("LOGIN", "Este login já existe!");
                    ViewBag.Usuarios = _appUsuario.ListarUsuariosSemLogin();
                    ViewBag.Login = _appLogin.ListarPorId(login.Id);
                    return View("Editar");
                }

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