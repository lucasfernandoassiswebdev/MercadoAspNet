using System.Collections.Generic;
using MercadoAplicacao.EstoqueApp;
using MercadoDominio.Entidades;
using MercadoDominio.Contrato;

namespace Mercado.Aplicacao.EstoqueApp
{
    public class EstoqueAplicacao : IEstoqueAplicacao
    {
        private readonly IEstoqueRepositorio _repositorio;

        public EstoqueAplicacao(IEstoqueRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void Salvar(Estoque estoque)
        {
            _repositorio.Salvar(estoque);
        }

        public void Excluir(Estoque estoque)
        {
            _repositorio.Excluir(estoque);
        }

        public IEnumerable<Estoque> ListarTodos()
        {
            return _repositorio.ListarTodos();
        }

        public Estoque ListarPorId(int id)
        {
            return _repositorio.ListarPorId(id);
        }

        public decimal? BuscaQuantidadeProduto(int idProduto)
        {
            return _repositorio.BuscaQuantidadeProduto(idProduto);
        }

        public int RetornaIdEstoque(int IdProduto)
        {
            return _repositorio.RetornaIdEstoque(IdProduto);
        }
    }
}
