using Mercado.Aplicacao.FabricanteApp;
using Mercado.Dominio;
using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class FabricanteController : Controller
    {
        private FabricanteAplicacao appFabricante;

        public FabricanteController()
        {
            appFabricante = FabricanteAplicacaoConstrutor.FabricanteAplicacaoADO();
        }

        public ActionResult Index()
        {
            var listaDeFabricantes = appFabricante.ListarTodos();
            return View(listaDeFabricantes);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                var appProduto = FabricanteAplicacaoConstrutor.FabricanteAplicacaoADO();
                appProduto.Salvar(fabricante);
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Editar(string id)
        {
            var fabricante = appFabricante.ListarPorId(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                var appFabricante = FabricanteAplicacaoConstrutor.FabricanteAplicacaoADO();
                appFabricante.Salvar(fabricante);
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Detalhes(string id)
        {
            var fabricante = appFabricante.ListarPorId(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        public ActionResult Excluir(string id)
        {
            var fabricante = appFabricante.ListarPorId(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(string id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var fabricante = appFabricante.ListarPorId(id);
            appFabricante.Excluir(fabricante);
            return RedirectToAction("Index");
        }
    }
}