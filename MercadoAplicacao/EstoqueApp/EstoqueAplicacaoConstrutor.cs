using Mercado.RepositorioADO;

namespace Mercado.Aplicacao.EstoqueApp
{
    public class EstoqueAplicacaoConstrutor
    {
        public static EstoqueAplicacao EstoqueAplicacaoADO()
        { 
            return new EstoqueAplicacao(new EstoqueRepositorioADO());
        }
    }
}
