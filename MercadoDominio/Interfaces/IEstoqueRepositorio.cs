using Mercado.Dominio;
using Mercado.Dominio.Contrato;

namespace MercadoDominio.Contrato
{
    public interface IEstoqueRepositorio : IRepositorio<Estoque>
    {
        decimal? BuscaQuantidadeProduto(int idProduto);
        int RetornaIdEstoque(int IdProduto);
    }
}
