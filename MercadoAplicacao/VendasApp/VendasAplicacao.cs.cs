using MercadoDominio.Entidades;
using MercadoDominio.Entidades.Entidades.Contrato;
using System.Collections.Generic;

namespace Mercado.Aplicacao.VendasApp
{
    public class VendasAplicacao
    {
        private readonly IRepositorio<Venda> repositorio;

        public VendasAplicacao(IRepositorio<Venda> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Venda venda)
        {
            repositorio.Salvar(venda);
        }

        public void Excluir(Venda venda)
        {
            repositorio.Excluir(venda);
        }

        public IEnumerable<Venda> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Venda ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}
