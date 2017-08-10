using Mercado.Aplicacao.EstoqueApp;
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
        private EstoqueAplicacao appEstoque;

        public VendaController()
        {
            appVendas = VendasAplicacaoConstrutor.VendaoAplicacaoADO();
            appProdutos = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
            appUsuarios = UsuarioAplicacaoConstrutor.UsuarioAplicacaoADO();
            appEstoque = EstoqueAplicacaoConstrutor.EstoqueAplicacaoADO();
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
            //Chamar o repositório passando o Id do Produto para buscar sua quantidade no estoque
            /*
                Injeção de Dependência
                Dependency Injection

                - SimpleInjector
             */
            var quantidade = appEstoque.BuscaQuantidadeProduto(venda.IdProduto);

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
                    IdProduto = venda.IdProduto,
                    Quantidade = (decimal)novoEstoque
                };

                appEstoque.Salvar(estoque);

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
                var appUsuario = VendasAplicacaoConstrutor.VendaoAplicacaoADO();
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

            ViewBag.Funcionario = appUsuarios.ListarPorId(venda.IdFuncionario);
            return View(venda);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var venda = appVendas.ListarPorId(id);
            appVendas.Excluir(venda);
            ViewBag.Funcionario = appUsuarios.ListarPorId(venda.IdFuncionario);
            return RedirectToAction("Index");
        }
    }
}