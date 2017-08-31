using System.Collections.Generic;
using MercadoDominio.Entidades;

namespace MercadoDominio.Interfaces
{
    public interface IProdutoRepositorio
    {
        void Salvar(Produto produto);
        void Excluir(Produto produto);
        IEnumerable<Produto> ListarTodos();
        Produto ListarPorId(int id);
    }
}
