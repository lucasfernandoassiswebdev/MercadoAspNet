using MercadoAplicacao.EstoqueApp;
using MercadoAplicacao.ProdutoApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Linq;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Produtos
{
    public class EstoqueController : AuthController
    {
        private readonly IEstoqueAplicacao _appEstoque;
        private readonly  IProdutoAplicacao _appProdutos;

        public EstoqueController(IEstoqueAplicacao estoque, IProdutoAplicacao produto)
        {
            _appEstoque = estoque;
            _appProdutos = produto;
        }

        public ActionResult Index()
        {
            var listaDoEstoque = _appEstoque.ListarTodos();

            ViewBag.Produtos = _appProdutos.ListarTodos();
            return View(listaDoEstoque);
        }

        public ActionResult Cadastrar()
        {
            var produtos = _appProdutos.ListarProdutosForaEstoque();

            if (produtos != null && produtos.Any())
            {
                ViewBag.Produtos = produtos;
                return View();
            }

            var listaDoEstoque = _appEstoque.ListarTodos();
            ModelState.AddModelError("ESTOQUE","Não há novos produtos a serem cadastrados no estoque!");
            return View("Index",listaDoEstoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                if(estoque.IdProduto == 0) { 
                    ModelState.AddModelError("ESTOQUE","Todos os produtos já foram cadastrados no estoque!");
                    ViewBag.Produtos = _appProdutos.ListarProdutosForaEstoque();
                    return View("Cadastrar");
                }

                _appEstoque.Salvar(estoque);

                return RedirectToAction("Index");
            }

            ViewBag.Produtos = _appProdutos.ListarTodos();
            return View(estoque);
        }

        public ActionResult Editar(int id)
        {
            var estoque = _appEstoque.ListarPorId(id);
            if (estoque == null)
                return HttpNotFound();

            ViewBag.Produto = _appProdutos.ListarPorId(estoque.IdProduto);
            return View(estoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                _appEstoque.Salvar(estoque);

                return RedirectToAction("Index");
            }

            ViewBag.Produto = _appProdutos.ListarPorId(estoque.IdProduto);

            return View(estoque);
        }

        public ActionResult Excluir(int id)
        {
            var estoque = _appEstoque.ListarPorId(id);
            if (estoque == null)
                return HttpNotFound();

            ViewBag.Produto = _appProdutos.ListarPorId(id);
            return View(estoque);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var estoque = _appEstoque.ListarPorId(id);
            _appEstoque.Excluir(estoque);

            ViewBag.Produto = _appProdutos.ListarPorId(id);
            return RedirectToAction("Index");
        }
    }
}