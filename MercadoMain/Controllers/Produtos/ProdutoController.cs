﻿using Mercado.Aplicacao.DistribuidorApp;
using Mercado.Aplicacao.FabricanteApp;
using Mercado.Aplicacao.ProdutoApp;
using MercadoDominio.Entidades;
using System.Web.Mvc;
using MercadoMain.Controllers;

namespace ProjetoMercado.Controllers
{
    public class ProdutoController : AuthController
    {
        private ProdutoAplicacao appProduto;
        private FabricanteAplicacao appFabricante;
        private DistribuidorAplicacao appDistribuidores;

        public ProdutoController()
        {
            appProduto = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
            appFabricante = FabricanteAplicacaoConstrutor.FabricanteAplicacaoADO();
            appDistribuidores = DistribuidorAplicacaoConstrutor.DistribuidorAplicacaoADO();
        }
        
        public ActionResult Index()
        {
            var listaDeProdutos = appProduto.ListarTodos();
            return View(listaDeProdutos);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Imagem == null)
                    produto.Imagem = "padrao.jpg";

                var appProduto = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
                appProduto.Salvar(produto);
                return RedirectToAction("Index");
            }

            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();

            return View(produto);
        }

        public ActionResult Editar(int id)
        {
            var produto = appProduto.ListarPorId(id);
            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();
            if (produto == null)
                return HttpNotFound();
            if (produto.Imagem != null)
                @ViewBag.Foto = produto.Imagem;
            else
                @ViewBag.Foto = "C:\\Users\\user\\Desktop\\imagens\\padrao.png";
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Imagem == null)
                    produto.Imagem = "C:\\Users\\user\\Desktop\\imagens\\padrao.jpg";
                var appDistribuidor = ProdutoAplicacaoConstrutor.ProdutoAplicacaoADO();
                appDistribuidor.Salvar(produto);
                return RedirectToAction("Index");
            }
            ViewBag.Fabricantes = appFabricante.ListarTodos();
            ViewBag.Distribuidores = appDistribuidores.ListarTodos();
            return View(produto);
        }

        public ActionResult Detalhes(int id)
        {
            var produto = appProduto.ListarPorId(id);
            if (produto == null)
                return HttpNotFound();

            ViewBag.Fabricantes = appFabricante.ListarPorId(produto.IdFabricante);
            ViewBag.Distribuidores = appDistribuidores.ListarPorId(produto.IdDistribuidor);
            return View(produto);
        }

        public ActionResult Excluir(int id)
        {
            var produto = appProduto.ListarPorId(id);

            if (produto == null)
                return HttpNotFound();
            ViewBag.Fabricantes = appFabricante.ListarPorId(produto.IdFabricante);
            ViewBag.Distribuidores = appDistribuidores.ListarPorId(produto.IdDistribuidor);
            return View(produto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var produto = appProduto.ListarPorId(id);
            appProduto.Excluir(produto);
            return RedirectToAction("Index");
        }
    }
}