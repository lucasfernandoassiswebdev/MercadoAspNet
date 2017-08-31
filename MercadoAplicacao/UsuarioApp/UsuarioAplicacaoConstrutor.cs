using MercadoRepositorioADO.Repositorios;

namespace MercadoAplicacao.UsuarioApp
{
    public class UsuarioAplicacaoConstrutor
    {
        public static UsuarioAplicacao UsuarioAplicacaoADO()
        {
            return new UsuarioAplicacao(new UsuarioRepositorioADO());
        }
    }
}
