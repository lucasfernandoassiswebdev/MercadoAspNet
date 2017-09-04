using MercadoAplicacao.LoginApp;
using MercadoAplicacao.UsuarioApp;
using MercadoAplicacao.VendasApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Usuarios
{
    public class UsuarioController : AuthController
    {
        private readonly IUsuarioAplicacao _appUsuario;
        private readonly ILoginAplicacao _appLogin;
        private readonly IVendasAplicacao _appVenda;

        public UsuarioController(IUsuarioAplicacao usuario, ILoginAplicacao login, IVendasAplicacao venda)
        {
            _appUsuario = usuario;
            _appLogin = login;
            _appVenda = venda;
        }

        public ActionResult Index()
        {
            var listaDeUsuarios = _appUsuario.ListarTodos();
            return View(listaDeUsuarios);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var equals = _appUsuario.VerificaExistenciaSimilar(usuario);
                if (equals == 1)
                {
                    ModelState.AddModelError("USUARIO", "Já existe usuário com este mesmo nome e nível!");
                    return View("Cadastrar");
                }

                _appUsuario.Salvar(usuario);
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public ActionResult Editar(int id)
        {
            var usuario = _appUsuario.ListarPorId(id);
            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var equals = _appUsuario.VerificaExistenciaSimilar(usuario);
                if (equals == 1)
                {
                    ModelState.AddModelError("USUARIO", "Já existe usuário com este mesmo nome e nível!");
                    return View("Editar");
                }

                _appUsuario.Salvar(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult Detalhes(int id)
        {
            var usuario = _appUsuario.ListarPorId(id);
            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        public ActionResult Excluir(int id)
        {
            var usuario = _appUsuario.ListarPorId(id);
            if (usuario == null)
                return HttpNotFound();
            
            return View(usuario);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var equal = _appLogin.VerificaLogin(id);
            if (equal == 1)
            {
                ModelState.AddModelError("USUARIO", "Você não pode excluir este usuário antes de excluir seu login!");
                var usuario = _appUsuario.ListarPorId(id);
                return View(usuario);
            }

            equal = _appVenda.VerificaVenda(id);
            if (equal == 1)
            {
                ModelState.AddModelError("USUARIO", "Você não pode excluir este usuário antes de excluir as vendas que ele realizou!");
                var usuario = _appUsuario.ListarPorId(id);
                return View(usuario);
            }

            _appUsuario.Excluir(id);

            return RedirectToAction("Index");
        }
    }
}