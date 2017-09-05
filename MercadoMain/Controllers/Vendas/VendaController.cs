using MercadoAplicacao.EstoqueApp;
using MercadoAplicacao.ProdutoApp;
using MercadoAplicacao.UsuarioApp;
using MercadoAplicacao.VendasApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Vendas
{
    public class VendaController : AuthController
    {
        private readonly IVendasAplicacao _appVendas;
        private readonly IProdutoAplicacao _appProdutos;
        private readonly IUsuarioAplicacao _appUsuarios;
        private readonly IEstoqueAplicacao _appEstoque;

        public VendaController(IEstoqueAplicacao estoqueAplicacao, IUsuarioAplicacao usuario, IProdutoAplicacao produto, IVendasAplicacao venda)
        {
            _appVendas = venda;
            _appProdutos = produto;
            _appUsuarios = usuario;
            _appEstoque = estoqueAplicacao;
        }

        public ActionResult Index()
        {
            var listaDeVendas = _appVendas.ListarTodos();
            return View(listaDeVendas);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Produtos = _appProdutos.ListarTodos();
            ViewBag.Funcionario = _appUsuarios.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Venda venda)
        {
            var quantidade = _appEstoque.BuscaQuantidadeProduto(venda.IdProduto);
            var IdEstoque = _appEstoque.RetornaIdEstoque(venda.IdProduto);
            decimal? novoEstoque = quantidade - venda.Quantidade;

            if (quantidade == null)
                ModelState.AddModelError("VENDA", "Não foi encontrado estoque para o produto informado!");
            else if (quantidade < venda.Quantidade)
                ModelState.AddModelError("VENDA", "Quantidade excedeu o estoque!");

            if (ModelState.IsValid)
            {
                _appVendas.Salvar(venda);

                var estoque = new Estoque
                {
                    Id = IdEstoque,
                    IdProduto = venda.IdProduto,
                    Quantidade = (decimal)novoEstoque
                };
                _appEstoque.Salvar(estoque);

                return RedirectToAction("Index");
            }

            ViewBag.Produtos = _appProdutos.ListarTodos();
            ViewBag.Funcionario = _appUsuarios.ListarTodos();
            return View(venda);
        }

        public ActionResult Editar(int id)
        {
            var venda = _appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();

            ViewBag.Produtos = _appProdutos.ListarTodos();
            ViewBag.Funcionarios = _appUsuarios.ListarTodos();
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Venda venda)
        {
            var quantidade = _appEstoque.BuscaQuantidadeProduto(venda.IdProduto);
            var IdEstoque = _appEstoque.RetornaIdEstoque(venda.IdProduto);
            decimal? novoEstoque = quantidade - venda.Quantidade;

            if (quantidade == null)
                ModelState.AddModelError("VENDA", "Não foi encontrado estoque para o produto informado!");
            else if (quantidade < venda.Quantidade)
                ModelState.AddModelError("VENDA", "Quantidade excedeu o estoque!");

            if (ModelState.IsValid)
            {
                _appVendas.Salvar(venda);
                var estoque = new Estoque
                {
                    Id = IdEstoque,
                    IdProduto = venda.IdProduto,
                    Quantidade = (decimal)novoEstoque
                };

                _appEstoque.Salvar(estoque);
                return RedirectToAction("Index");
            }

            ViewBag.Produtos = _appProdutos.ListarTodos();
            ViewBag.Funcionarios = _appUsuarios.ListarTodos();
            return View(venda);
        }

        public ActionResult Detalhes(int id)
        {
            var venda = _appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();

            ViewBag.Produtos = _appProdutos.ListarTodos();
            ViewBag.Funcionario = _appUsuarios.ListarPorId(venda.IdFuncionario);
            return View(venda);
        }

        public ActionResult Excluir(int id)
        {
            var venda = _appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();

            ViewBag.Funcionario = _appUsuarios.ListarPorId(venda.IdFuncionario);
            return View(venda);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var venda = _appVendas.ListarPorId(id);
            _appVendas.Excluir(venda);

            ViewBag.Funcionario = _appUsuarios.ListarPorId(venda.IdFuncionario);
            return RedirectToAction("Index");
        }
    }
}