using Mercado.Dominio;
using System.Collections.Generic;
using MercadoDominio.Contrato;
using MercadoRepositorioADO.Extensoes;

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
                var cmd = contexto.ExecutaComando("ListarEstoque");

                var estoques = new List<Estoque>();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        estoques.Add(new Estoque()
                        {
                            IdProduto = reader.ReadAsInt("IdProduto"),
                            Produto = new Produto
                            {
                                Nome = reader.ReadAsString("Nome"),
                                Fabricante = new Fabricante
                                {
                                    Nome = reader.ReadAsString("Fabricante")
                                },
                                Distribuidor = new Distribuidor
                                {
                                    Nome = reader.ReadAsString("Distribuidor")
                                }
                            },
                            Quantidade = reader.ReadAsDecimal("Quantidade")
                        });

                return estoques;
            }
        }

        public Estoque ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("ListarEstoquePorIdProduto");
                cmd.Parameters.AddWithValue("@Id", id);
                var estoque = new Estoque();
                using (var reader = cmd.ExecuteReader())
                     if (reader.Read())
                        return new Estoque()
                        {
                            Id = reader.ReadAsInt("Id"),
                            IdProduto = reader.ReadAsInt("IdProduto"),
                            Quantidade = reader.ReadAsDecimal("Quantidade")
                        };
                return null;
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
