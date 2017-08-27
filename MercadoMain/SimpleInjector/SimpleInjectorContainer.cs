using Mercado.Aplicacao.DistribuidorApp;
using SimpleInjector;

namespace MercadoMain.SimpleInjector
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            var container = new Container();

            //registrando as implementações
            container.Register<DistribuidorAplicacaoConstrutor, DistribuidorAplicacao>();

            container.Verify();
            return container;
        }
    }
}