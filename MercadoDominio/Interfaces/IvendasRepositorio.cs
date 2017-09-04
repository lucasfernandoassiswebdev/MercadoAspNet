using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IVendasRepositorio
    {
        void Salvar(Venda venda);
        void Excluir(Venda venda);
        IEnumerable<Venda> ListarTodos();
        Venda ListarPorId(int id);
        int VerificaVenda(int idFuncionario);
    }
}
