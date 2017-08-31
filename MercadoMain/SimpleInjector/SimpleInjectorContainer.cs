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

            container.Register<IEstoqueRepositorio, EstoqueRepositorioADO>();
            container.Register<IEstoqueAplicacao, EstoqueAplicacao>();

            container.Verify();
            return container;
        }
    }
}
