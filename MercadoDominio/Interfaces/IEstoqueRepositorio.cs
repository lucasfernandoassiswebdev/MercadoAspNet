using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IEstoqueRepositorio
    {
        void Salvar(Estoque estoque);
        void Excluir(Estoque estoque);
        IEnumerable<Estoque> ListarTodos();
        Estoque ListarPorId(int id);
        decimal? BuscaQuantidadeProduto(int idProduto);
        int RetornaIdEstoque(int idProduto);
    }
}
