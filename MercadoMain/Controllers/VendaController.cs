using Mercado.Aplicacao.VendasApp;
using Mercado.Dominio;
using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class VendaController : Controller
    {
        private VendasAplicacao appVendas;

        public VendaController()
        {
            appVendas = VendasAplicacaoConstrutor.VendaoAplicacaoADO();
        }

        public ActionResult Index()
        {
            var listaDeVendas = appVendas.ListarTodos();
            return View(listaDeVendas);
        }

        public ActionResult Cadastrar()
        {
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
            return View(venda);
        }

        public ActionResult Editar(string id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();

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
            return View(venda);
        }

        public ActionResult Detalhes(string id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();

            return View(venda);
        }

        public ActionResult Excluir(string id)
        {
            var venda = appVendas.ListarPorId(id);

            if (venda == null)
                return HttpNotFound();

            return View(venda);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(string id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var venda = appVendas.ListarPorId(id);
            appVendas.Excluir(venda);
            return RedirectToAction("Index");
        }
    }
}