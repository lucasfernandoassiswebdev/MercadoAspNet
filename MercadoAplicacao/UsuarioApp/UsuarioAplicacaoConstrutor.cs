using Mercado.RepositorioADO;

namespace Mercado.Aplicacao.UsuarioApp
{
    public class UsuarioAplicacaoConstrutor
    {
        public static UsuarioAplicacao UsuarioAplicacaoADO()
        {
            return new UsuarioAplicacao(new UsuarioRepositorioADO());
        }
    }
}
