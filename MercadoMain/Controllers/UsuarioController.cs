using Mercado.Aplicacao.UsuarioApp;
using Mercado.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioAplicacao appUsuario;

        public UsuarioController()
        {
            appUsuario = UsuarioAplicacaoConstrutor.UsuarioAplicacaoADO();
        }

        public ActionResult Index()
        {
            var listaDeUsuarios = appUsuario.ListarTodos();
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
                var appProduto = UsuarioAplicacaoConstrutor.UsuarioAplicacaoADO();
                appProduto.Salvar(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult Editar(string id)
        {
            var usuario = appUsuario.ListarPorId(id);

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
                var appUsuario = UsuarioAplicacaoConstrutor.UsuarioAplicacaoADO();
                appUsuario.Salvar(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult Detalhes(string id)
        {
            var usuario = appUsuario.ListarPorId(id);

            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        public ActionResult Excluir(string id)
        {
            var usuario = appUsuario.ListarPorId(id);

            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(string id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var usuario = appUsuario.ListarPorId(id);
            appUsuario.Excluir(usuario);
            return RedirectToAction("Index");
        }
    }
}