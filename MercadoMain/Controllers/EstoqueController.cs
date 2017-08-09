using Mercado.Aplicacao.EstoqueApp;
using Mercado.Aplicacao.ProdutoApp;
using Mercado.Dominio;
using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class EstoqueController : Controller
    {
        private EstoqueAplicacao appEstoque;
        private ProdutoAplicacao appProdutos;

        public EstoqueController()
        {
            appProdutos = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
            appEstoque = EstoqueAplicacaoConstrutor.EstoqueAplicacaoADO();
        }

        public ActionResult Index()
        {
            var listaDoEstoque = appEstoque.ListarTodos();
            ViewBag.Produtos = appProdutos.ListarTodos();
            return View(listaDoEstoque);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Produtos = appProdutos.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                var appProduto = EstoqueAplicacaoConstrutor.EstoqueAplicacaoADO();
                appProduto.Salvar(estoque);
                return RedirectToAction("Index");
            }
            ViewBag.Produtos = appProdutos.ListarTodos();
            return View(estoque);
        }

        public ActionResult Editar(string id)
        {
            var estoque = appEstoque.ListarPorId(id);
            if (estoque == null)
                return HttpNotFound();
            ViewBag.Produto = appProdutos.ListarPorId(id);
            return View(estoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                var appFabricante = EstoqueAplicacaoConstrutor.EstoqueAplicacaoADO();
                appFabricante.Salvar(estoque);
                return RedirectToAction("Index");
            }
            ViewBag.Produto = appProdutos.ListarPorId(estoque.IdProduto.ToString());
            return View(estoque);
        }

        public ActionResult Detalhes(string id)
        {
            var estoque = appEstoque.ListarPorId(id);

            if (estoque == null)
                return HttpNotFound();
            ViewBag.Produto = appProdutos.ListarPorId(id);
            return View(estoque);
        }

        public ActionResult Excluir(string id)
        {
            var estoque = appEstoque.ListarPorId(id);

            if (estoque == null)
                return HttpNotFound();
            ViewBag.Produto = appProdutos.ListarPorId(id);
            return View(estoque);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(string id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var estoque = appEstoque.ListarPorId(id);
            appEstoque.Excluir(estoque);
            ViewBag.Produto = appProdutos.ListarPorId(id);
            return RedirectToAction("Index");
        }
    }
}