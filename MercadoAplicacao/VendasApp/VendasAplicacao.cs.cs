using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.VendasApp
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
