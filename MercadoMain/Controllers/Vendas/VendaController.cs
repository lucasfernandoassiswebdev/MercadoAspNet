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
        private VendasAplicacao appVendas;
        private ProdutoAplicacao appProdutos;
        private readonly IUsuarioAplicacao _appUsuarios;
        private readonly IEstoqueAplicacao _appEstoque;

        public VendaController(IEstoqueAplicacao estoqueAplicacao, IUsuarioAplicacao usuario)
        {
            appVendas = VendasAplicacaoConstrutor.VendaoAplicacaoADO();
            appProdutos = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
            _appUsuarios = usuario;
            _appEstoque = estoqueAplicacao;
        }

        public ActionResult Index()
        {
            var listaDeVendas = appVendas.ListarTodos();
            return View(listaDeVendas);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionario = _appUsuarios.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Venda venda)
        {
            //Chamar o repositório passando o Id do Produto para buscar sua quantidade no estoque
            /*
                Injeção de Dependência
                Dependency Injection

                - SimpleInjector
             */
            var quantidade = _appEstoque.BuscaQuantidadeProduto(venda.IdProduto);
            var IdEstoque = _appEstoque.RetornaIdEstoque(venda.IdProduto);
            decimal? novoEstoque = quantidade - venda.Quantidade;

            if (quantidade == null)
                ModelState.AddModelError("VENDA", "Não foi encontrado estoque para o produto informado!");
            else if (quantidade < venda.Quantidade)
                ModelState.AddModelError("VENDA", "Quantidade excedeu o estoque!");

            if (ModelState.IsValid)
            {
                appVendas.Salvar(venda);
                var estoque = new Estoque
                {
                    Id = IdEstoque,
                    IdProduto = venda.IdProduto,
                    Quantidade = (decimal)novoEstoque
                };

                _appEstoque.Salvar(estoque);

                return RedirectToAction("Index");
            }
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionario = _appUsuarios.ListarTodos();
            return View(venda);
        }

        public ActionResult Editar(int id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionarios = _appUsuarios.ListarTodos();
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Venda venda)
        {
            if (ModelState.IsValid)
            {
                var appUsuario = VendasAplicacaoConstrutor.VendaoAplicacaoADO();
                appUsuario.Salvar(venda);
                return RedirectToAction("Index");
            }
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionarios = _appUsuarios.ListarTodos();
            return View(venda);
        }

        public ActionResult Detalhes(int id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();
            ViewBag.Produtos = appProdutos.ListarTodos();
            ViewBag.Funcionario = _appUsuarios.ListarPorId(venda.IdFuncionario);
            return View(venda);
        }

        public ActionResult Excluir(int id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();

            ViewBag.Funcionario = _appUsuarios.ListarPorId(venda.IdFuncionario);
            return View(venda);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var venda = appVendas.ListarPorId(id);
            appVendas.Excluir(venda);
            ViewBag.Funcionario = _appUsuarios.ListarPorId(venda.IdFuncionario);
            return RedirectToAction("Index");
        }
    }
}