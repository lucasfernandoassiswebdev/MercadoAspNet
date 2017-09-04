using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;

namespace MercadoRepositorioADO.Repositorios
{
    public class EstoqueRepositorioADO : IEstoqueRepositorio
    {
        private Contexto.Contexto _contexto;

        private void Insert(Estoque estoque)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("InsereEstoque");
                cmd.Parameters.AddWithValue("@IdProduto", estoque.IdProduto);
                cmd.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Estoque estoque)
        {
           using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("AlteraEstoque");
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
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ExcluirEstoque");
                cmd.Parameters.AddWithValue("@IdProduto", estoque.IdProduto);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Estoque> ListarTodos()
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ListarEstoque");

                var estoques = new List<Estoque>();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        estoques.Add( new Estoque()
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
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ListarEstoquePorIdProduto");
                cmd.Parameters.AddWithValue("@IdProduto", id);
                
                using (var reader = cmd.ExecuteReader())
                     if (reader.Read())
                        return new Estoque
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
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("BuscaQProduto");
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return reader.ReadAsDecimal("Quantidade");

                return null;
            }
        }

        public int RetornaIdEstoque(int idProduto)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("RetornaIdEstoquePeloProduto");
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return reader.ReadAsInt("Id");

                return 0;
            }
        }
    }
}
