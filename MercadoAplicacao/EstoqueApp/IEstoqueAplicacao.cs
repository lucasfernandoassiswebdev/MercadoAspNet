using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoAplicacao.EstoqueApp
{
    public interface IEstoqueAplicacao
    {
        void Salvar(Estoque estoque);
        void Excluir(Estoque estoque);
        IEnumerable<Estoque> ListarTodos();
        Estoque ListarPorId(int id);
        decimal? BuscaQuantidadeProduto(int idProduto);
        int RetornaIdEstoque(int idProduto);
    }
}
