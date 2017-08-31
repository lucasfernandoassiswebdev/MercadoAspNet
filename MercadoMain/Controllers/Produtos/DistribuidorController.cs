using MercadoAplicacao.DistribuidorApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Produtos
{
    public class DistribuidorController : AuthController
    {
        private  readonly IDistribuidorAplicacao _appDistribuidor;

        public DistribuidorController(IDistribuidorAplicacao distribuidor)
        {
            _appDistribuidor = distribuidor;
        }

        public ActionResult Index()
        {
            var listaDeDistribuidores = _appDistribuidor.ListarTodos();
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
                _appDistribuidor.Salvar(distribuidor);
                return RedirectToAction("Index");
            }
            return View(distribuidor);
        }

        public ActionResult Editar(int id)
        {
            var distribuidor = _appDistribuidor.ListarPorId(id);

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
                _appDistribuidor.Salvar(distribuidor);
                return RedirectToAction("Index");
            }
            return View(distribuidor);
        }

        public ActionResult Detalhes(int id)
        {
            var distribuidor = _appDistribuidor.ListarPorId(id);

            if (distribuidor == null)
                return HttpNotFound();

            return View(distribuidor);
        }

        public ActionResult Excluir(int id)
        {
            var distribuidor = _appDistribuidor.ListarPorId(id);

            if (distribuidor == null)
                return HttpNotFound();

            return View(distribuidor);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var distribuidor = _appDistribuidor.ListarPorId(id);
            _appDistribuidor.Excluir(distribuidor);
            return RedirectToAction("Index");
        }
    }
}