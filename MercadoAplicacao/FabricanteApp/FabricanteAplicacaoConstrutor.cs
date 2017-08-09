using Mercado.RepositorioADO;

namespace Mercado.Aplicacao.FabricanteApp
{
    public class FabricanteAplicacaoConstrutor
    {
        public static FabricanteAplicacao FabricanteAplicacaoADO()
        {
            return new FabricanteAplicacao(new FabricanteRepositorioADO());
        }
    }
}
