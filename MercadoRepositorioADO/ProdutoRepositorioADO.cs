using Mercado.Dominio.Contrato;
using System.Linq;
using Mercado.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mercado.RepositorioADO
{
    public class ProdutoRepositorioADO : IRepositorio<Produto>
    {
        private Contexto contexto;

        private void Insert(Produto produto)
        {
            var strQuery = "";
            strQuery += " INSERT INTO DBProdutos(Nome,Valor,Fabricante,Distribuidor)" + 
                       $" VALUES('{produto.Nome}','{produto.Valor}','{produto.IdFabricante}','{produto.IdDistribuidor}')";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Produto produto)
        {
            var strQuery = "";
            strQuery += "UPDATE DBProdutos SET ";
            //String Interpolation = $
            strQuery += $" Nome = '{produto.Nome}', " +
                        $" Valor = '{produto.Valor}', " + 
                        $" Fabricante = '{produto.IdFabricante}', " +
                        $" Distribuidor = '{produto.IdDistribuidor}' " +
                        $" WHERE Id = '{produto.Id}' ";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
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
                var strQuery = $"DELETE FROM DBProdutos WHERE Id = '{produto.Id}'";
                contexto.ExecutaComando(strQuery);
            }
        }

        public IEnumerable<Produto> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " select p.Id, p.Nome, p.Valor, f.Nome as 'nFabricante', d.Nome as 'nDistribuidor' from DBProdutos p " +
                                "inner join DBFabricantes f on f.Id = p.Fabricante " +
                                "inner join DBDistribuidores d on d.Id = p.Distribuidor";
                //byte, short, int, long, ubyte, ushort, uint, ulong
                var produtos = new List<Produto>();
                using (var reader = contexto.ExecutaComandoComRetorno(strQuery))
                    while (reader.Read())
                        produtos.Add(new Produto()
                        {
                            Id = reader.ReadAsInt("Id"),
                            Nome = reader.ReadAsString("Nome"),
                            Valor = reader.ReadAsDecimal("Valor"),
                            Fabricante = new Fabricante
                            {
                                Nome = reader.ReadAsString("nFabricante")
                            },
                            Distribuidor = new Distribuidor
                            {
                                Nome = reader.ReadAsString("nDistribuidor")
                            }
                        });

                return produtos;
            }
        }

        public Produto ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = $" SELECT * FROM DBProdutos WHERE Id = {id}";

                using (var reader = contexto.ExecutaComandoComRetorno(strQuery))
                    if (reader.Read())
                        return new Produto
                        {
                            Id = reader.ReadAsInt("Id"),
                            Nome = reader.ReadAsString("Nome"),
                            IdFabricante = reader.ReadAsInt("Fabricante"),
                            IdDistribuidor = reader.ReadAsInt("Distribuidor"),
                            Valor = reader.ReadAsDecimal("Valor")
                        };

                return null;
            }
        }
    }

    public static class DatabaseExtension
    {
        public static int? ReadAsIntNull(this SqlDataReader r, string name)
        {
            var ordinal = r.GetOrdinal(name);
            return r.IsDBNull(ordinal) ? (int?)null : r.GetInt32(ordinal);
        }

        public static int ReadAsInt(this SqlDataReader r, string name)
        {
            return r.GetInt32(r.GetOrdinal(name));
        }

        public static string ReadAsString(this SqlDataReader r, string name)
        {
            return r.GetString(r.GetOrdinal(name));
        }

        public static decimal ReadAsDecimal(this SqlDataReader r, string name)
        {
            return r.GetDecimal(r.GetOrdinal(name));
        }
    }
}
