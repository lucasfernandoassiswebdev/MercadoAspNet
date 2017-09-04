using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.FabricanteApp
{
    public class FabricanteAplicacao : IFabricanteAplicacao
    {
        private readonly IFabricanteRepositorio _appFabricante;

        public FabricanteAplicacao(IFabricanteRepositorio fabricante)
        {
            _appFabricante = fabricante;
        }

        public void Salvar(Fabricante fabricante)
        {
            _appFabricante.Salvar(fabricante);
        }

        public void Excluir(Fabricante fabricante)
        {
            _appFabricante.Excluir(fabricante);
        }

        public IEnumerable<Fabricante> ListarTodos()
        {
            return _appFabricante.ListarTodos();
        }

        public Fabricante ListarPorId(int id)
        {
            return _appFabricante.ListarPorId(id);
        }

        public int VerificaExistenciaSimilar(Fabricante fabricante)
        {
            return _appFabricante.VerificaExistenciaSimilar(fabricante);
        }

        public int VerificaFabricante(int id)
        {
            return _appFabricante.VerificaFabricante(id);
        }
    }
}
