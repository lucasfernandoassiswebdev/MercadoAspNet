using Mercado.Dominio.Contrato;
using System.Linq;
using Mercado.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using MercadoDominio.Contrato;
using System;

namespace Mercado.RepositorioADO
{
    public class EstoqueRepositorioADO : IEstoqueRepositorio
    {
        private Contexto contexto;

        private void Insert(Estoque estoque)
        {
            var strQuery = "";
            strQuery += " INSERT INTO DBEstoque(IdProduto,Quantidade) " + 
                          $" VALUES('{estoque.IdProduto}','{estoque.Quantidade}')";

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Estoque estoque)
        {
            var strQuery = "";
            strQuery += " UPDATE DBEstoque SET " +
                          $" IdProduto = " + estoque.IdProduto +
                          $", Quantidade = " + estoque.Quantidade +
                          $" WHERE IdProduto = " + estoque.IdProduto;
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
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
                var strQuery = $"DELETE FROM DBEstoque WHERE IdProduto = '{estoque.IdProduto}'";
                contexto.ExecutaComando(strQuery);
            }
        }

        public IEnumerable<Estoque> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " select p.id as 'IdProduto', p.Nome as 'Nome', d.Nome as 'Distribuidor', " +
                               $" f.Nome as 'Fabricante', e.Quantidade from DBProdutos p " +
                               $" inner join DBEstoque e on e.IdProduto = p.Id" +
                               $" inner join DBFabricantes f on f.Id = p.Fabricante" +
                               $" inner join DBDistribuidores d on d.Id = p.Distribuidor";
                var estoques = new List<Estoque>();
                using (var reader = contexto.ExecutaComandoComRetorno(strQuery))
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
                var strQuery = $" SELECT * FROM DBEstoque WHERE IdProduto = {id}";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader).FirstOrDefault();
            }
        }

        private List<Estoque> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var estoques = new List<Estoque>();
            while (reader.Read())
            {
                var temObjeto = new Estoque()
                {
                    Id = reader.ReadAsInt("Id"),
                    IdProduto = reader.ReadAsInt("IdProduto"),
                    Quantidade = reader.ReadAsDecimal("Quantidade")
                };
                estoques.Add(temObjeto);
            }

            reader.Close();
            return estoques;
        }

        public decimal? BuscaQuantidadeProduto(int idProduto)
        {
            using (contexto = new Contexto())
            {
                var strQuery = "SELECT e.Id, e.Quantidade FROM DBEstoque e " +
                               "INNER JOIN DBProdutos p on p.Id = e.IdProduto " +
                               $"WHERE IdProduto = {idProduto}";

                using (var r = contexto.ExecutaComandoComRetorno(strQuery))
                    if (r.Read())
                        return r.ReadAsDecimal("Quantidade");

                return null;
            }
        }

        public int RetornaIdEstoque(int IdProduto)
        {
            using (contexto = new Contexto())
            {
                var strQuery = "SELECT e.Id FROM DBEstoque e " +
                               "INNER JOIN DBProdutos p on p.Id = e.IdProduto " +
                               $"WHERE IdProduto = {IdProduto}";

                using (var r = contexto.ExecutaComandoComRetorno(strQuery))
                    if (r.Read())
                        return r.ReadAsInt("Id");

                return 0;
            }
        }
    }
}
