using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.ProdutoApp
{
    public class ProdutoAplicacao : IProdutoAplicacao
    {
        private readonly IProdutoRepositorio _appProduto;

        public ProdutoAplicacao(IProdutoRepositorio produto)
        {
            _appProduto = produto;
        }

        public void Salvar(Produto produto)
        {
            _appProduto.Salvar(produto);
        }

        public void Excluir(Produto produto)
        {
            _appProduto.Excluir(produto);
        }

        public IEnumerable<Produto> ListarTodos()
        {
            return _appProduto.ListarTodos();
        }

        public Produto ListarPorId(int id)
        {
            return _appProduto.ListarPorId(id);
        }
    }
}
