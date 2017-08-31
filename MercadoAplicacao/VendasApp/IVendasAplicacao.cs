using System.Collections.Generic;
using MercadoDominio.Entidades;

namespace MercadoAplicacao.VendasApp
{
    public interface IVendasAplicacao
    {
        void Salvar(Venda venda);
        void Excluir(Venda venda);
        IEnumerable<Venda> ListarTodos();
        Venda ListarPorId(int id);
    }
}
