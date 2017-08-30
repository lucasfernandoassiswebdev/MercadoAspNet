using System.Collections.Generic;
using MercadoDominio.Entidades;

namespace MercadoAplicacao.EstoqueApp
{
    public interface IEstoqueAplicacao
    {
        void Salvar(Estoque estoque);
        void Excluir(Estoque estoque);
        IEnumerable<Estoque> ListarTodos();
        Estoque ListarPorId(int id);
        decimal? BuscaQuantidadeProduto(int idProduto);
        int RetornaIdEstoque(int IdProduto);
    }
}
