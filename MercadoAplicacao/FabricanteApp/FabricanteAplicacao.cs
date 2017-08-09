using System.Collections.Generic;
using Mercado.Dominio;
using Mercado.Dominio.Contrato;

namespace Mercado.Aplicacao.FabricanteApp
{
    public class FabricanteAplicacao
    {
        private readonly IRepositorio<Fabricante> repositorio;

        public FabricanteAplicacao(IRepositorio<Fabricante> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Fabricante fabricante)
        {
            repositorio.Salvar(fabricante);
        }

        public void Excluir(Fabricante fabricante)
        {
            repositorio.Excluir(fabricante);
        }

        public IEnumerable<Fabricante> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Fabricante ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}
