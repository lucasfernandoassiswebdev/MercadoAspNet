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
                var fabricantes = _appFabricante.ListarTodos();

                foreach (var fabricanteA in fabricantes)
                {
                    if (fabricanteA.Nome == fabricante.Nome)
                    {
                        ModelState.AddModelError("FABRICANTE", "Já existe um fabricante com este mesmo nome!");
                        return View("Cadastrar");
                    }
                }

                if (fabricante.Nome.Length > 75)
                {
                    ModelState.AddModelError("FABRICANTE", "Você está ultrapssando o número máximo de caracteres!");
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
        public ActionResult ExcluirConfirmado(int id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var fabricante = _appFabricante.ListarPorId(id);
            _appFabricante.Excluir(fabricante);
            return RedirectToAction("Index");
        }
    }
}