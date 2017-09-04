using MercadoAplicacao.FabricanteApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Produtos
{
    public class FabricanteController : AuthController
    {
        private readonly  IFabricanteAplicacao _appFabricante;

        public FabricanteController(IFabricanteAplicacao fabricante)
        {
            _appFabricante = fabricante;
        }

        public ActionResult Index()
        {
            var listaDeFabricantes = _appFabricante.ListarTodos();
            return View(listaDeFabricantes);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Fabricantes = _appFabricante.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                var equal = _appFabricante.VerificaExistenciaSimilar(fabricante);

                if (equal == 1)
                {
                    ModelState.AddModelError("FABRICANTE", "Já existe um fabricante com este mesmo nome!");
                    return View("Cadastrar");
                }

                _appFabricante.Salvar(fabricante);
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Editar(int id)
        {
            var fabricante = _appFabricante.ListarPorId(id);

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
                var equal = _appFabricante.VerificaExistenciaSimilar(fabricante);

                if (equal == 1)
                {
                    ModelState.AddModelError("FABRICANTE", "Já existe um fabricante com este mesmo nome ou você não fez nenhuma alteração!");
                    var fabricanteA = _appFabricante.ListarPorId(fabricante.Id);
                    return View(fabricanteA);
                }

                _appFabricante.Salvar(fabricante);
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Detalhes(int id)
        {
            var fabricante = _appFabricante.ListarPorId(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        public ActionResult Excluir(int id)
        {
            var fabricante = _appFabricante.ListarPorId(id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var equals = _appFabricante.VerificaFabricante(id);
            if (equals == 1)
            {
                ModelState.AddModelError("FABRICANTE", "Este fabricante tem produtos relacionados e por isso não pode ser excluído!");
                var fabricanteA = _appFabricante.ListarPorId(id);
                return View(fabricanteA);
            }

            var fabricante = _appFabricante.ListarPorId(id);
            _appFabricante.Excluir(fabricante);
            return RedirectToAction("Index");
        }
    }
}