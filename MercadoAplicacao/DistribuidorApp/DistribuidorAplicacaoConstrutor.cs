using MercadoRepositorioADO.Repositorios;

namespace MercadoAplicacao.DistribuidorApp
{
    public class DistribuidorAplicacaoConstrutor
    {
        public static DistribuidorAplicacao DistribuidorAplicacaoADO()
        {
            return new DistribuidorAplicacao(new DistribuidorRepositorioADO());
        }
    }
}
