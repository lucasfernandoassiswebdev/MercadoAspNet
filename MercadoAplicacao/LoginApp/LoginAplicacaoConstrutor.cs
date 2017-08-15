using MercadoRepositorioADO;
using MercadoRepositorioADO.Repositorios;

namespace MercadoAplicacao.LoginApp
{
    public class LoginAplicacaoConstrutor
    {
        public static LoginAplicacao LoginAplicacaoADO()
        {
            return new LoginAplicacao(new LoginRepositorioADO());
        }
    }
}
