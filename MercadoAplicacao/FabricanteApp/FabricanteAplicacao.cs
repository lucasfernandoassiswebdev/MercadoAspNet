using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.FabricanteApp
{
    public class FabricanteAplicacao : IFabricanteAplicacao
    {
        private readonly IFabricanteRepositorio _fabricante;

        public FabricanteAplicacao(IFabricanteRepositorio fabricante)
        {
            _fabricante = fabricante;
        }

        public void Salvar(Fabricante fabricante)
        {
            _fabricante.Salvar(fabricante);
        }

        public void Excluir(Fabricante fabricante)
        {
            _fabricante.Excluir(fabricante);
        }

        public IEnumerable<Fabricante> ListarTodos()
        {
            return _fabricante.ListarTodos();
        }

        public Fabricante ListarPorId(int id)
        {
            return _fabricante.ListarPorId(id);
        }
    }
}
