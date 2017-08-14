using Mercado.RepositorioADO;

namespace Mercado.Aplicacao.VendasApp
{
    public class VendasAplicacaoConstrutor
    {
        public static VendasAplicacao VendaoAplicacaoADO()
        {
            return new VendasAplicacao(new VendaRepositorioADO());
        }
    }
}
