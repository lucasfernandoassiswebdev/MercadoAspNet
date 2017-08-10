using Mercado.Dominio.Contrato;
using System.Linq;
using Mercado.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Mercado.RepositorioADO
{
    public class DistribuidorRepositorioADO : IRepositorio<Distribuidor>
    {
        private Contexto contexto;

        private void Insert(Distribuidor distribuidor)
        {
            var strQuery = "";
            strQuery += " INSERT INTO DBDistribuidores(Nome)" +
                          $" VALUES('{distribuidor.Nome}') ";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Distribuidor distribuidor)
        {
            var strQuery = "";
            strQuery += "UPDATE DBDistribuidores SET " +
                         $" Nome = '{distribuidor.Nome}'" +
                         $" WHERE Id = {distribuidor.Id}";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Distribuidor distribuidor)
        {
            if (distribuidor.Id > 0)
                Alterar(distribuidor);
            else
                Insert(distribuidor);
        }

        public void Excluir(Distribuidor distribuidor)
        {
            using (contexto = new Contexto())
            {
                var strQuery = $"DELETE FROM DBDistribuidores WHERE Id = '{distribuidor.Id}'";
                contexto.ExecutaComando(strQuery);
            }
        }

        public IEnumerable<Distribuidor> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM DBDistribuidores";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader);
            }
        }

        public Distribuidor ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = $" SELECT * FROM DBDistribuidores WHERE Id = {id}";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader).FirstOrDefault();
            }
        }

        private List<Distribuidor> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var distribuidores = new List<Distribuidor>();
            while (reader.Read())
            {
                var temObjeto = new Distribuidor()
                {
                    Id = reader.ReadAsInt("Id"),
                    Nome = reader.ReadAsString("Nome")
                };
                distribuidores.Add(temObjeto);
            }

            reader.Close();
            return distribuidores;
        }
    }
}
