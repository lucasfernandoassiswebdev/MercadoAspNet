using Mercado.RepositorioADO;

namespace Mercado.Aplicacao.DistribuidorApp
{
    public class DistribuidorAplicacaoConstrutor
    {
        public static DistribuidorAplicacao DistribuidorAplicacaoADO()
        {
            return new DistribuidorAplicacao(new DistribuidorRepositorioADO());
        }
    }
}
