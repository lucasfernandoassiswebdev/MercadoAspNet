using MercadoAplicacao.DistribuidorApp;
using MercadoAplicacao.EstoqueApp;
using MercadoDominio.Interfaces;
using MercadoRepositorioADO.Repositorios;
using SimpleInjector;
using System.Reflection;

namespace MercadoMain.SimpleInjector
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            var container = new Container();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            //estoque
            container.Register<IEstoqueRepositorio, EstoqueRepositorioADO>();
            container.Register<IEstoqueAplicacao, EstoqueAplicacao>();
            //distribuidor
            container.Register<IDistribuidorRepositorio, DistribuidorRepositorioADO>();
            container.Register<IDistribuidorAplicacao, DistribuidorAplicacao>();
            
            container.Verify();
            return container;
        }
    }
}
