using MercadoDominio.Entidades;
using MercadoDominio.Entidades.Entidades.Contrato;

namespace MercadoDominio.Contrato
{
    public interface IEstoqueRepositorio : IRepositorio<Estoque>
    {
        decimal? BuscaQuantidadeProduto(int idProduto);
        int RetornaIdEstoque(int IdProduto);
    }
}
