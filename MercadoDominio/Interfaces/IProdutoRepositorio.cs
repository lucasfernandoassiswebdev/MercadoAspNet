using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IProdutoRepositorio
    {
        void Salvar(Produto produto);
        void Excluir(Produto produto);
        IEnumerable<Produto> ListarTodos();
        Produto ListarPorId(int id);
        int VerificaExistenciaSimilar(Produto produto);
    }
}
