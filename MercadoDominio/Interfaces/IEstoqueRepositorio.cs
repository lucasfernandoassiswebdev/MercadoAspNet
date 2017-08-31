using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IEstoqueRepositorio
    {
        void Salvar(Estoque entidade);
        void Excluir(Estoque entidade);
        IEnumerable<Estoque> ListarTodos();
        Estoque ListarPorId(int Id);
        decimal? BuscaQuantidadeProduto(int idProduto);
        int RetornaIdEstoque(int IdProduto);
    }
}
