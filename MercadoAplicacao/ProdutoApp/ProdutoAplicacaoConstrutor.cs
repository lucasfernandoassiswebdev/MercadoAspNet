using MercadoRepositorioADO.Repositorios;

namespace MercadoAplicacao.ProdutoApp
{
    public class ProdutoAplicacaoConstrutor
    {
        public static ProdutoAplicacao ProdutoAplicacaoADO()
        {
            return new ProdutoAplicacao(new ProdutoRepositorioADO());
        }
    }
}
