using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.EstoqueApp
{
    public class EstoqueAplicacao : IEstoqueAplicacao
    {
        private readonly IEstoqueRepositorio _appEstoque;

        public EstoqueAplicacao(IEstoqueRepositorio estoque)
        {
            _appEstoque = estoque;
        }

        public void Salvar(Estoque estoque)
        {
            _appEstoque.Salvar(estoque);
        }

        public void Excluir(Estoque estoque)
        {
            _appEstoque.Excluir(estoque);
        }

        public IEnumerable<Estoque> ListarTodos()
        {
            return _appEstoque.ListarTodos();
        }

        public Estoque ListarPorId(int id)
        {
            return _appEstoque.ListarPorId(id);
        }

        public decimal? BuscaQuantidadeProduto(int idProduto)
        {
            return _appEstoque.BuscaQuantidadeProduto(idProduto);
        }

        public int RetornaIdEstoque(int idProduto)
        {
            return _appEstoque.RetornaIdEstoque(idProduto);
        }
    }
}
