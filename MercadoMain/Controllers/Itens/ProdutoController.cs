using MercadoAplicacao.DistribuidorApp;
using MercadoAplicacao.FabricanteApp;
using MercadoAplicacao.ProdutoApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Produtos
{
    public class ProdutoController : AuthController
    {
        private readonly IProdutoAplicacao _appProduto;
        private readonly IFabricanteAplicacao _appFabricantes;
        private readonly IDistribuidorAplicacao _appDistribuidores;

        public ProdutoController(IDistribuidorAplicacao distribuidor, IFabricanteAplicacao fabricante, IProdutoAplicacao produto)
        {
            _appProduto = produto;
            _appFabricantes = fabricante;
            _appDistribuidores = distribuidor;
        }
        
        public ActionResult Index()
        {
            var listaDeProdutos = _appProduto.ListarTodos();
            return View(listaDeProdutos);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Fabricantes = _appFabricantes.ListarTodos();
            ViewBag.Distribuidores = _appDistribuidores.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Produto produto, HttpPostedFileBase uploadImagem)
        {
            if (ModelState.IsValid)
            {
                if (uploadImagem == null)
                    produto.Imagem = "padrao.jpg";
                else
                {
                    //extensões permitidas
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var checkextension = Path.GetExtension(uploadImagem.FileName).ToLower();

                    if (!allowedExtensions.Contains(checkextension))
                    {
                        ModelState.AddModelError("PRODUTO", "Selecione apenas IMAGENS que estejam nos formatos jpg, png, ou gif!");
                        ViewBag.Fabricantes = _appFabricantes.ListarTodos();
                        ViewBag.Distribuidores = _appDistribuidores.ListarTodos();
                        return View("Cadastrar");
                    }

                    //copiando a imagem para a aplicação
                    produto.Imagem = uploadImagem.FileName;
                    string pathSave = $"{Server.MapPath("~/Imagens/")}{produto.Imagem}";
                    uploadImagem.SaveAs(pathSave);
                }

                var equal = _appProduto.VerificaExistenciaSimilar(produto);
                if (equal == 1)
                {
                    ModelState.AddModelError("PRODUTO", "Já existe um produto com este mesmo nome deste mesmo fabricante!");
                    ViewBag.Fabricantes = _appFabricantes.ListarTodos();
                    ViewBag.Distribuidores = _appDistribuidores.ListarTodos();
                    return View("Cadastrar");
                }

                _appProduto.Salvar(produto);
                return RedirectToAction("Index");
            }

            ViewBag.Fabricantes = _appFabricantes.ListarTodos();
            ViewBag.Distribuidores = _appDistribuidores.ListarTodos();
            return View(produto);
        }

        public ActionResult Editar(int id)
        {
            var produto = _appProduto.ListarPorId(id);
            ViewBag.Fabricantes = _appFabricantes.ListarTodos();
            ViewBag.Distribuidores = _appDistribuidores.ListarTodos();

            if (produto == null)
                return HttpNotFound();

            if (produto.Imagem != null)
                @ViewBag.Foto = produto.Imagem;
            else
                @ViewBag.Foto = "padrao.jpg";

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Produto produto, HttpPostedFileBase uploadImagem)
        {
            if (ModelState.IsValid)
            {
                if (uploadImagem != null)
                {
                    //extensões permitidas
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var checkextension = Path.GetExtension(uploadImagem.FileName).ToLower();

                    if (!allowedExtensions.Contains(checkextension))
                    {
                        ModelState.AddModelError("PRODUTO", "Selecione apenas IMAGENS que estejam nos formatos jpg, png, ou gif!");
                        ViewBag.Fabricantes = _appFabricantes.ListarTodos();
                        ViewBag.Distribuidores = _appDistribuidores.ListarTodos();
                        return View("Cadastrar");
                    }

                    //copiando a imagem para a aplicação
                    produto.Imagem = uploadImagem.FileName;
                    string pathSave = $"{Server.MapPath("~/Imagens/")}{produto.Imagem}";
                    uploadImagem.SaveAs(pathSave);
                }

                var equal = _appProduto.VerificaExistenciaSimilar(produto);
                if (equal == 1)
                {
                    ModelState.AddModelError("PRODUTO", "Já existe um produto com este mesmo nome e fabricante ou você não fez nenhuma alteração!");
                    var produtoA = _appProduto.ListarPorId(produto.Id);
                    ViewBag.Fabricantes = _appFabricantes.ListarTodos();
                    ViewBag.Distribuidores = _appDistribuidores.ListarTodos();
                    return View(produtoA);
                }

                _appProduto.Salvar(produto);
                return RedirectToAction("Index");
            }

            ViewBag.Fabricantes = _appFabricantes.ListarTodos();
            ViewBag.Distribuidores = _appDistribuidores.ListarTodos();
            return View(produto);
        }

        public ActionResult Detalhes(int id)
        {
            var produto = _appProduto.ListarPorId(id);
            if (produto == null)
                return HttpNotFound();

            ViewBag.Fabricantes = _appFabricantes.ListarPorId(produto.IdFabricante);
            ViewBag.Distribuidores = _appDistribuidores.ListarPorId(produto.IdDistribuidor);
            return View(produto);
        }

        public ActionResult Excluir(int id)
        {
            var produto = _appProduto.ListarPorId(id);
            if (produto == null)
                return HttpNotFound();

            ViewBag.Fabricantes = _appFabricantes.ListarPorId(produto.IdFabricante);
            ViewBag.Distribuidores = _appDistribuidores.ListarPorId(produto.IdDistribuidor);
            return View(produto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var produto = _appProduto.ListarPorId(id);
            _appProduto.Excluir(produto);

            return RedirectToAction("Index");
        }

        public ActionResult ExportarExcel()
        {
            //criando uma instância de ExcelPackage (uma ferramenta do EPPlus)
            ExcelPackage excel = new ExcelPackage();
            //dando o nome a página de "index"
            var workSheet = excel.Workbook.Worksheets.Add("Index");
            //setando propriedades da altura das linhas
            workSheet.DefaultRowHeight = 12;
            
            //definindo estilos e propriedades
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            //formatações para a coluna de valor do produto 
            workSheet.Column(4).Style.Numberformat.Format = "R$ #,##0.00;(#,##0.00)";
            workSheet.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //nome das colunas da tabela que será gerada (1ª linha)
            workSheet.Cells[1, 1].Value = "Nome";
            workSheet.Cells[1, 2].Value = "Fabricante";
            workSheet.Cells[1, 3].Value = "Distribuidor";
            workSheet.Cells[1, 4].Value = "Valor";

            for (int i = 1; i < 5; i++)
            {
                workSheet.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                workSheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells[1, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }

            //listando produtos a serem preenchidos na tabela
            var produtos = _appProduto.ListarTodos();
            //linha de início
            int recordIndex = 2;
            foreach (var produto in produtos)
            {
                if (recordIndex % 2 == 0)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        workSheet.Cells[recordIndex,i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[recordIndex,i].Style.Fill.BackgroundColor.SetColor(Color.White);

                        workSheet.Cells[recordIndex, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[recordIndex, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[recordIndex, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[recordIndex, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    }
                }
                else
                {
                    for (int i = 1; i < 5; i++)
                    {
                        workSheet.Cells[recordIndex, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[recordIndex, i].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                        workSheet.Cells[recordIndex, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[recordIndex, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[recordIndex, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[recordIndex, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    }
                }

                //preenchendo a tabela do Excel
                workSheet.Cells[recordIndex, 1].Value = produto.Nome;
                workSheet.Cells[recordIndex, 2].Value = produto.Fabricante.Nome;
                workSheet.Cells[recordIndex, 3].Value = produto.Distribuidor.Nome;
                workSheet.Cells[recordIndex, 4].Value = produto.Valor;
                recordIndex++;
            }

            //autofit seta o a largura das colunas automaticamente
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();

            string excelName = "ListaDeProdutos";
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //nome dado ao arquivo Excel que será gerado
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }
    }
}