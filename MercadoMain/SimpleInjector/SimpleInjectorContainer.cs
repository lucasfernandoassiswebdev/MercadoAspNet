using Mercado.Aplicacao.DistribuidorApp;
using Mercado.Aplicacao.EstoqueApp;
using Mercado.RepositorioADO;
using MercadoAplicacao.DistribuidorApp;
using MercadoAplicacao.EstoqueApp;
using MercadoDominio.Contrato;
using SimpleInjector;

namespace MercadoMain.SimpleInjector
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            var container = new Container();

            //registrando as implementações
            //estoque
            container.Register<IEstoqueAplicacao, EstoqueAplicacao>();
            container.Register<IEstoqueRepositorio, EstoqueRepositorioADO>();
          
           
            container.Verify();
            return container;
        }
    }
}