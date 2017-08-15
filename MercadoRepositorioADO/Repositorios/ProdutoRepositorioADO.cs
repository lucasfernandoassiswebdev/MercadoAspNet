using Mercado.Dominio.Contrato;
using Mercado.Dominio;
using System.Collections.Generic;

namespace Mercado.RepositorioADO
{
    public class ProdutoRepositorioADO : IRepositorio<Produto>
    {
        private Contexto contexto;

        private void Insert(Produto produto)
        {
           using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereProduto");
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@Fabricante", produto.IdFabricante);
                cmd.Parameters.AddWithValue("@Distribuidor", produto.IdDistribuidor);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Produto produto)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("AlteraProduto");
                cmd.Parameters.AddWithValue("@Id", produto.Id);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@Fabricante", produto.IdFabricante);
                cmd.Parameters.AddWithValue("@Distribuidor", produto.IdDistribuidor);
                cmd.ExecuteNonQuery();
            }
        }

        public void Salvar(Produto produto)
        {
            //se passar o id ele vai alterar, se não passar ele vai inserir um novo aluno
            if (produto.Id > 0)
                Alterar(produto);
            else
                Insert(produto);
        }

        public void Excluir(Produto produto)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("ExcluiProduto");
                cmd.Parameters.AddWithValue("@Id", produto.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Produto> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComandoComRetorno("ListaProdutos");
                var produtos = new List<Produto>();
                
                while (cmd.Read())
                    produtos.Add(new Produto()
                    {
                        Id = cmd.ReadAsInt("Id"),
                        Nome = cmd.ReadAsString("Nome"),
                        Valor = cmd.ReadAsDecimal("Valor"),
                        Fabricante = new Fabricante
                        {
                            Nome = cmd.ReadAsString("nFabricante")
                        },
                        Distribuidor = new Distribuidor
                        {
                            Nome = cmd.ReadAsString("nDistribuidor")
                        }
                    });

                return produtos;
            }
        }

        public Produto ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComandoComRetorno("ListaProdutoPorId");
                cmd.Parameters.AddWithValue("@Id", id);

                var produto = new Produto();
                while (cmd.Read())
                    return new Produto
                    {
                        Id = cmd.ReadAsInt("Id"),
                        Nome = cmd.ReadAsString("Nome"),
                        IdFabricante = cmd.ReadAsInt("Fabricante"),
                        IdDistribuidor = cmd.ReadAsInt("Distribuidor"),
                        Valor = cmd.ReadAsDecimal("Valor"),
                        Fabricante = new Fabricante
                        {
                            Nome = cmd.ReadAsString("NomeFabricante")
                        },
                        Distribuidor = new Distribuidor
                        {
                            Nome = cmd.ReadAsString("NomeDistribuidor")
                        }
                    };
                 cmd.Close();
                 return produto;
            }
        }
    }
}
