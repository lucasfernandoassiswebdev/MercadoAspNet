using System.Collections.Generic;
using Mercado.Dominio;
using MercadoDominio.Contrato;

namespace Mercado.Aplicacao.EstoqueApp
{
    public class EstoqueAplicacao
    {
        public readonly IEstoqueRepositorio repositorio;

        public EstoqueAplicacao(IEstoqueRepositorio repo)
        {
            repositorio = repo;
        }

        public void Salvar(Estoque estoque)
        {
            repositorio.Salvar(estoque);
        }

        public void Excluir(Estoque estoque)
        {
            repositorio.Excluir(estoque);
        }

        public IEnumerable<Estoque> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Estoque ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }

        public decimal? BuscaQuantidadeProduto(int idProduto)
        {
            return repositorio.BuscaQuantidadeProduto(idProduto);
        }

        public int RetornaIdEstoque(int IdProduto)
        {
            return repositorio.RetornaIdEstoque(IdProduto);
        }
    }
}
