using System.Linq;
using Mercado.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using MercadoDominio.Contrato;

namespace Mercado.RepositorioADO
{
    public class EstoqueRepositorioADO : IEstoqueRepositorio
    {
        private Contexto contexto;

        private void Insert(Estoque estoque)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereEstoque");
                cmd.Parameters.AddWithValue("@IdProduto", estoque.IdProduto);
                cmd.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Estoque estoque)
        {
           using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("AlteraEstoque");
                cmd.Parameters.AddWithValue("@IdProduto", estoque.IdProduto);
                cmd.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                cmd.ExecuteNonQuery();
            }
        }

        public void Salvar(Estoque estoque)
        {
          if(estoque.Id > 0)
          {
            Alterar(estoque);
          }
          else
          {
            Insert(estoque);
          }
        }

        public void Excluir(Estoque estoque)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("ExcluirEstoque");
                cmd.Parameters.AddWithValue("@IdProduto", estoque.IdProduto);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Estoque> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComandoComRetorno("ListarEstoque");

                var estoques = new List<Estoque>();

                while (cmd.Read())
                estoques.Add(new Estoque()
                {
                    IdProduto = cmd.ReadAsInt("IdProduto"),
                    Produto = new Produto
                    {
                        Nome = cmd.ReadAsString("Nome"),
                        Fabricante = new Fabricante
                        {
                            Nome = cmd.ReadAsString("Fabricante")
                        },
                        Distribuidor = new Distribuidor
                        {
                            Nome = cmd.ReadAsString("Distribuidor")
                        }
                    },
                   Quantidade = cmd.ReadAsDecimal("Quantidade")
                });

                return estoques;
            }
        }

        public Estoque ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComandoComRetorno("ListarEstoquePorIdProduto");
                cmd.Parameters.AddWithValue("@Id", id);

                var estoque = new Estoque();
                while (cmd.Read())
                {
                    var temObjeto = new Estoque()
                    {
                        Id = cmd.ReadAsInt("Id"),
                        IdProduto = cmd.ReadAsInt("IdProduto"),
                        Quantidade = cmd.ReadAsDecimal("Quantidade")
                    };
                    estoque.Add(temObjeto);
                }

                cmd.Close();
                return estoque;
            }
        }

        public decimal? BuscaQuantidadeProduto(int idProduto)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComandoComRetorno("BuscaQProduto");
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);

                if (cmd.Read())
                        return cmd.ReadAsDecimal("Quantidade");

                return null;
            }
        }

        public int RetornaIdEstoque(int IdProduto)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComandoComRetorno("RetornaIdEstoquePeloProduto");
                cmd.Parameters.AddWithValue("@IdProduto", IdProduto);

                if (cmd.Read())
                    return cmd.ReadAsInt("Id");

                return 0;
            }
        }
    }
}
