using Mercado.Aplicacao.ProdutoApp;
using Mercado.Aplicacao.UsuarioApp;
using Mercado.Aplicacao.VendasApp;
using Mercado.Dominio;
using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class VendaController : Controller
    {
        private VendasAplicacao appVendas;
        private ProdutoAplicacao appProdutos;
        private UsuarioAplicacao appUsuarios;

        public VendaController()
        {
            appVendas = VendasAplicacaoConstrutor.VendaoAplicacaoADO();
            appProdutos = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
            appUsuarios = UsuarioAplicacaoConstrutor.UsuarioAplicacaoADO();
        }

        public ActionResult Index()
        {
            var listaDeVendas = appVendas.ListarTodos();
            return View(listaDeVendas);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionario = appUsuarios.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Venda venda)
        {
            if (ModelState.IsValid)
            {
                var appProduto = VendasAplicacaoConstrutor.VendaoAplicacaoADO();
                appProduto.Salvar(venda);
                return RedirectToAction("Index");
            }
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionario = appUsuarios.ListarTodos();
            return View(venda);
        }

        public ActionResult Editar(int id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionarios = appUsuarios.ListarTodos();
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Venda venda)
        {
            if (ModelState.IsValid)
            {
                var appUsuario = VendasAplicacaoConstrutor.VendaoAplicacaoADO() ;
                appUsuario.Salvar(venda);
                return RedirectToAction("Index");
            }
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionarios = appUsuarios.ListarTodos();
            return View(venda);
        }

        public ActionResult Detalhes(int id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionario = appUsuarios.ListarPorId(venda.IdFuncionario);
            return View(venda);
        }

        public ActionResult Excluir(int id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();
            return View(venda);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var venda = appVendas.ListarPorId(id);
            appVendas.Excluir(venda);
            return RedirectToAction("Index");
        }
    }
}