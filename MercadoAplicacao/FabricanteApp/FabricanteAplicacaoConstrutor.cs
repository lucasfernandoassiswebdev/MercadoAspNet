using MercadoRepositorioADO.Repositorios;

namespace MercadoAplicacao.FabricanteApp
{
    public class FabricanteAplicacaoConstrutor
    {
        public static FabricanteAplicacao FabricanteAplicacaoADO()
        {
            return new FabricanteAplicacao(new FabricanteRepositorioADO());
        }
    }
}
