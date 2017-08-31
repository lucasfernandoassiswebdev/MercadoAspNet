using MercadoAplicacao.UsuarioApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Usuarios
{
    public class UsuarioController : AuthController
    {
        private readonly IUsuarioAplicacao _appUsuario;

        public UsuarioController(IUsuarioAplicacao usuario)
        {
            _appUsuario = usuario;
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
        public ActionResult ExcluirConfirmado(int id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var usuario = _appUsuario.ListarPorId(id);
            _appUsuario.Excluir(usuario);
            return RedirectToAction("Index");
        }
    }
}