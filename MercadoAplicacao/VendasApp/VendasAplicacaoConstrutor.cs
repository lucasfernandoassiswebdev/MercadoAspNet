using MercadoRepositorioADO.Repositorios;

namespace MercadoAplicacao.VendasApp
{
    public class VendasAplicacaoConstrutor
    {
        public static VendasAplicacao VendaoAplicacaoADO()
        {
            return new VendasAplicacao(new VendaRepositorioADO());
        }
    }
}
