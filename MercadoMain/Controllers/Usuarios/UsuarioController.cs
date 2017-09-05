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

        public ActionResult Excluir(int id)
        {
            _appUsuario.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}