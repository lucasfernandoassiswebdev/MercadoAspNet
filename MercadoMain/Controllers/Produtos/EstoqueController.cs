﻿using MercadoAplicacao.EstoqueApp;
using MercadoAplicacao.ProdutoApp;
using MercadoDominio.Entidades;
using MercadoMain.Controllers.Autenticacao;
using System.Web.Mvc;

namespace MercadoMain.Controllers.Produtos
{
    public class EstoqueController : AuthController
    {
        private readonly IEstoqueAplicacao _appEstoque;
        private readonly  IProdutoAplicacao _appProdutos;

        public EstoqueController(IEstoqueAplicacao estoque, IProdutoAplicacao produto)
        {
            _appEstoque = estoque;
            _appProdutos = produto;
        }

        public ActionResult Index()
        {
            var listaDoEstoque = _appEstoque.ListarTodos();
            ViewBag.Produtos = _appProdutos.ListarTodos();
            return View(listaDoEstoque);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Produtos = _appProdutos.ListarTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                
                _appEstoque.Salvar(estoque);
                return RedirectToAction("Index");
            }
            ViewBag.Produtos = _appProdutos.ListarTodos();
            return View(estoque);
        }

        public ActionResult Editar(int id)
        {
            var estoque = _appEstoque.ListarPorId(id);
            if (estoque == null)
                return HttpNotFound();
            ViewBag.Produto = _appProdutos.ListarPorId(estoque.IdProduto);
            return View(estoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                _appEstoque.Salvar(estoque);
                return RedirectToAction("Index");
            }
            ViewBag.Produto = _appProdutos.ListarPorId(estoque.IdProduto);
            return View(estoque);
        }

        public ActionResult Detalhes(int id)
        {
            var estoque = _appEstoque.ListarPorId(id);

            if (estoque == null)
                return HttpNotFound();
            ViewBag.Produto = _appProdutos.ListarPorId(id);
            return View(estoque);
        }

        public ActionResult Excluir(int id)
        {
            var estoque = _appEstoque.ListarPorId(id);

            if (estoque == null)
                return HttpNotFound();
            ViewBag.Produto = _appProdutos.ListarPorId(id);
            return View(estoque);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)//pro c# esse método se chama excluirconfirmado mas pro ASP se chama Excluir, igual o de cima
        {
            var estoque = _appEstoque.ListarPorId(id);
            _appEstoque.Excluir(estoque);
            ViewBag.Produto = _appProdutos.ListarPorId(id);
            return RedirectToAction("Index");
        }
    }
}