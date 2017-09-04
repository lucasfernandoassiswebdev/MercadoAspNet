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

                var equal = _appDistribuidor.VerificaExistenciaSimilar(distribuidor);
                if (equal == 1)
                {
                    ModelState.AddModelError("DISTRIBUIDOR", "Já existe um distribuidor com este mesmo nome!");
                    return View("Cadastrar");
                }

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
                var equal = _appDistribuidor.VerificaExistenciaSimilar(distribuidor);

                if (equal == 1)
                {
                    ModelState.AddModelError("DISTRIBUIDOR", "Já existe um distribuidor com este mesmo nome ou você não fez nenhuma alteração!");
                    var distribuidorA = _appDistribuidor.ListarPorId(distribuidor.Id);
                    return View(distribuidorA);
                }

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
        public ActionResult ExcluirConfirmado(int id)
        {
            var equals = _appDistribuidor.VerificaDistribuidor(id);
            if (equals == 1)
            {
                ModelState.AddModelError("DISTRIBUIDOR", "Este distribuidor não pode ser excluído porque já está relacionado a algum produto!");
                var distribuidorA = _appDistribuidor.ListarPorId(id);
                return View(distribuidorA);
            }

            var distribuidor = _appDistribuidor.ListarPorId(id);
            _appDistribuidor.Excluir(distribuidor);
            return RedirectToAction("Index");
        }
    }
}