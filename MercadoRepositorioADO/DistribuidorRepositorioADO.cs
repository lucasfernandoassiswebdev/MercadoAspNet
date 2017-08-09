using Mercado.Dominio.Contrato;
using System.Linq;
using Mercado.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mercado.RepositorioADO
{
    public class DistribuidorRepositorioADO : IRepositorio<Distribuidor>
    {
        private Contexto contexto;

        private void Insert(Distribuidor distribuidor)
        {
            var strQuery = "";
            strQuery += " INSERT INTO DBDistribuidores(Nome)";
            strQuery += string.Format(" VALUES('{0}')",
                distribuidor.Nome
            );
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Distribuidor distribuidor)
        {
            var strQuery = "";
            strQuery += "UPDATE DBDistribuidores SET ";
            strQuery += string.Format(" Nome = '{0}'", distribuidor.Nome);
            strQuery += string.Format(" WHERE Id = {0}", distribuidor.Id);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Distribuidor distribuidor)
        {
            //se passar o id ele vai alterar, se não passar ele vai inserir um novo aluno
            if (distribuidor.Id > 0)
                Alterar(distribuidor);
            else
                Insert(distribuidor);
        }

        public void Excluir(Distribuidor distribuidor)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format("DELETE FROM DBDistribuidores WHERE Id = '{0}'", distribuidor.Id);
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

        public Distribuidor ListarPorId(string id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM DBDistribuidores WHERE Id = {0}", id);
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
                    Id = int.Parse(reader["Id"].ToString()),
                    Nome = reader["Nome"].ToString(),
                };
                distribuidores.Add(temObjeto);
            }

            reader.Close();
            return distribuidores;
        }
    }
}
