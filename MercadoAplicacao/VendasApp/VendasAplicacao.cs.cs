using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.VendasApp
{
    public class VendasAplicacao : IVendasAplicacao
    {
        private readonly IVendasRepositorio _appVendas;

        public VendasAplicacao(IVendasRepositorio venda)
        {
            _appVendas = venda;
        }

        public void Salvar(Venda venda)
        {
            _appVendas.Salvar(venda);
        }

        public void Excluir(Venda venda)
        {
            _appVendas.Excluir(venda);
        }

        public IEnumerable<Venda> ListarTodos()
        {
            return _appVendas.ListarTodos();
        }

        public Venda ListarPorId(int id)
        {
            return _appVendas.ListarPorId(id);
        }

        public int VerificaVenda(int idFuncionario)
        {
            return _appVendas.VerificaVenda(idFuncionario);
        }
    }
}
