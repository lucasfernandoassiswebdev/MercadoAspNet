using Mercado.RepositorioADO;

namespace Mercado.Aplicacao.ProdutoApp
{
    public class ProdutoAplicacaoConstrutor
    {
        public static ProdutoAplicacao ProdutoAplicacaoADO()
        {
            return new ProdutoAplicacao(new ProdutoRepositorioADO());
        }
    }
}
