using Mercado.Dominio;
using Mercado.Dominio.Contrato;
using System.Collections.Generic;

namespace Mercado.RepositorioADO
{
    public class VendaRepositorioADO : IRepositorio<Venda>
    {
        private Contexto contexto;

        private void Insert(Venda venda)
        {
            var strQuery = "";
            strQuery += " INSERT INTO DBVendas(IdProduto,Quantidade,Funcionario)";
            strQuery += $" VALUES('{venda.IdProduto}','{venda.Quantidade}','{venda.Funcionario}'";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Venda venda)
        {
            var strQuery = "";
            strQuery += "UPDATE DBVendas SET ";
            //String Interpolation = $
            strQuery += $" IdProduto = '{venda.IdProduto}', " +
                        $" Quantidade = '{venda.Quantidade}', " +
                        $" Funcionario = '{venda.Funcionario}', ";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Venda venda)
        {
            //se passar o id ele vai alterar, se não passar ele vai inserir um novo aluno
            if (venda.IdVenda > 0)
                Alterar(venda);
            else
                Insert(venda);
        }

        public void Excluir(Venda venda)
        {
            using (contexto = new Contexto())
            {
                var strQuery = $"DELETE FROM DBProdutos WHERE IdVenda = '{venda.IdVenda}'";
                contexto.ExecutaComando(strQuery);
            }
        }

        public IEnumerable<Venda> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " select v.IdVenda, p.Nome as 'Produto', v.Quantidade, u.Nome as 'Funcionario' from DBVendas v " +
                               " inner join DBUsuarios u on u.Id = v.Funcionario" +
                               " inner join DBUsuarios u on u.Id = v.Funcionario";
                //byte, short, int, long, ubyte, ushort, uint, ulong
                var vendas = new List<Venda>();
                using (var reader = contexto.ExecutaComandoComRetorno(strQuery))
                    while (reader.Read())
                        vendas.Add( new Venda()
                        {
                            IdVenda = reader.ReadAsInt("IdVenda"),
                            IdProduto = reader.ReadAsInt("IdProduto"),
                            Quantidade = reader.ReadAsDecimal("Quantidade"),
                            Funcionario = reader.ReadAsInt("Funcionario")
                        });

                return vendas;
            }
        }

        public Venda ListarPorId(string id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = $" SELECT * DBVendas WHERE IdVenda = {id}";

                using (var reader = contexto.ExecutaComandoComRetorno(strQuery))
                    if (reader.Read())
                        return new Venda
                        {
                            IdVenda = reader.ReadAsInt("IdVenda"),
                            IdProduto = reader.ReadAsInt("IdProduto"),
                            Quantidade = reader.ReadAsDecimal("Quantidade"),
                            Funcionario = reader.ReadAsInt("Funcionario"),
                        };

                return null;
            }
        }
    }

    
}
