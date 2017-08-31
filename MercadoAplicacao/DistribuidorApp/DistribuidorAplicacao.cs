using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.DistribuidorApp
{
    public class DistribuidorAplicacao
    {
        private readonly IRepositorio<Distribuidor> repositorio;

        public DistribuidorAplicacao(IRepositorio<Distribuidor> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Distribuidor distribuidor)
        {
            repositorio.Salvar(distribuidor);
        }

        public void Excluir(Distribuidor distribuidor)
        {
            repositorio.Excluir(distribuidor);
        }

        public IEnumerable<Distribuidor> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Distribuidor ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}
