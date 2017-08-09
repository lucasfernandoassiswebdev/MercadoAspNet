using Mercado.Aplicacao.DistribuidorApp;
using Mercado.Aplicacao.FabricanteApp;
using Mercado.Aplicacao.ProdutoApp;
using Mercado.Dominio;
using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutoAplicacao appProduto;
        private FabricanteAplicacao appFabricante;
        private DistribuidorAplicacao appDistribuidores;

        public ProdutoController()
        {
            appProduto = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
            appFabricante = FabricanteAplicacaoConstrutor.FabricanteAplicacaoADO();
            appDistribuidores = DistribuidorAplicacaoConstrutor.DistribuidorAplicacaoADO();
        }
        
        public ActionResult Index()
        {
            var listaDeProdutos = appProduto.ListarTodos();
            return View(listaDeProdutos);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var appProduto = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
                appProduto.Salvar(produto);
                return RedirectToAction("Index");
            }

            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();

            return View(produto);
        }

        public ActionResult Editar(string id)
        {
            var produto = appProduto.ListarPorId(id);
            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();
            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var appDistribuidor = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
                appDistribuidor.Salvar(produto);
                return RedirectToAction("Index");
            }
            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();
            return View(produto);
        }

        public ActionResult Detalhes(string id)
        {
            var produto = appProduto.ListarPorId(id);
            if (produto == null)
                return HttpNotFound();
            ViewBag.Fabricantes = appFabricante.ListarPorId(produto.IdFabricante.ToString());
            ViewBag.Distribuidores = appDistribuidores.ListarPorId(produto.IdDistribuidor.ToString());
            return View(produto);
        }

        public ActionResult Excluir(string id)
        {
            var produto = appProduto.ListarPorId(id);

            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(string id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var produto = appProduto.ListarPorId(id);
            appProduto.Excluir(produto);
            return RedirectToAction("Index");
        }
    }
}