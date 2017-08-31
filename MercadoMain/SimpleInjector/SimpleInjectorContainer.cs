using MercadoAplicacao.DistribuidorApp;
using MercadoAplicacao.EstoqueApp;
using MercadoAplicacao.FabricanteApp;
using MercadoAplicacao.LoginApp;
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
            //fabricante
            container.Register<IFabricanteRepositorio, FabricanteRepositorioADO>();
            container.Register<IFabricanteAplicacao, FabricanteAplicacao>();
            //login
            container.Register<ILoginRepositorio, LoginRepositorioADO>();
            container.Register<ILoginAplicacao, LoginAplicacao>();



            container.Verify();
            return container;
        }
    }
}
