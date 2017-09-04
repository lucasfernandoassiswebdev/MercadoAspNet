using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoAplicacao.VendasApp
{
    public interface IVendasAplicacao
    {
        void Salvar(Venda venda);
        void Excluir(Venda venda);
        IEnumerable<Venda> ListarTodos();
        Venda ListarPorId(int id);
        int VerificaVenda(int idFuncionario);
    }
}
