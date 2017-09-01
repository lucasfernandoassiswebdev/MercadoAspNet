using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoAplicacao.ProdutoApp
{
    public interface IProdutoAplicacao
    {
        void Salvar(Produto produto);
        void Excluir(Produto produto);
        IEnumerable<Produto> ListarTodos();
        Produto ListarPorId(int id);
        int VerificaExistenciaSimilar(Produto produto);
    }
}
