using Mercado.Dominio;
using Mercado.Dominio.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.Aplicacao.DistribuidorApp
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

        public Distribuidor ListarPorId(string id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}
