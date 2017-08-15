using Mercado.Aplicacao.DistribuidorApp;
using MercadoDominio.Entidades;
using System.Web.Mvc;

namespace ProjetoMercado.Controllers
{
    public class DistribuidorController : Controller
    {
        private DistribuidorAplicacao appDistribuidor;

        public DistribuidorController()
        {
            appDistribuidor = DistribuidorAplicacaoConstrutor.DistribuidorAplicacaoADO();
        }

        public ActionResult Index()
        {
            var listaDeDistribuidores = appDistribuidor.ListarTodos();
            return View(listaDeDistribuidores);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Distribuidor distribuidor)
        {
            if (ModelState.IsValid)
            {
                var appDistribuidor = DistribuidorAplicacaoConstrutor.DistribuidorAplicacaoADO();
                appDistribuidor.Salvar(distribuidor);
                return RedirectToAction("Index");
            }
            return View(distribuidor);
        }

        public ActionResult Editar(int id)
        {
            var distribuidor = appDistribuidor.ListarPorId(id);

            if (distribuidor == null)
                return HttpNotFound();

            return View(distribuidor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Distribuidor distribuidor)
        {
            if (ModelState.IsValid)
            {
                var appDistribuidor = DistribuidorAplicacaoConstrutor.DistribuidorAplicacaoADO();
                appDistribuidor.Salvar(distribuidor);
                return RedirectToAction("Index");
            }
            return View(distribuidor);
        }

        public ActionResult Detalhes(int id)
        {
            var distribuidor = appDistribuidor.ListarPorId(id);

            if (distribuidor == null)
                return HttpNotFound();

            return View(distribuidor);
        }

        public ActionResult Excluir(int id)
        {
            var distribuidor = appDistribuidor.ListarPorId(id);

            if (distribuidor == null)
                return HttpNotFound();

            return View(distribuidor);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var distribuidor = appDistribuidor.ListarPorId(id);
            appDistribuidor.Excluir(distribuidor);
            return RedirectToAction("Index");
        }
    }
}