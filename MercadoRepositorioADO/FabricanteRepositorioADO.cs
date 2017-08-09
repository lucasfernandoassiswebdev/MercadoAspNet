using Mercado.Dominio.Contrato;
using System.Linq;
using Mercado.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mercado.RepositorioADO
{
    public class FabricanteRepositorioADO : IRepositorio<Fabricante>
    {
        private Contexto contexto;

        private void Insert(Fabricante fabricante)
        {
            var strQuery = "";
            strQuery += " INSERT INTO DBFabricantes(Nome)";
            strQuery += string.Format(" VALUES('{0}')",
                fabricante.Nome
                );
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Fabricante fabricante)
        {
            var strQuery = "";
            strQuery += "UPDATE DBFabricantes SET ";
            strQuery += string.Format(" Nome = '{0}'", fabricante.Nome);
            strQuery += string.Format(" WHERE Id = {0}",fabricante.Id);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Fabricante fabricante)
        {
            //se passar o id ele vai alterar, se não passar ele vai inserir um novo aluno
            if (fabricante.Id > 0)
                Alterar(fabricante);
            else
                Insert(fabricante);
        }

        public void Excluir(Fabricante fabricante)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format("DELETE FROM DBFabricantes WHERE Id = '{0}'", fabricante.Id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public IEnumerable<Fabricante> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM DBFabricantes";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader);
            }
        }

        public Fabricante ListarPorId(string id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM DBFabricantes WHERE Id = {0}", id);
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader).FirstOrDefault();
            }
        }

        private List<Fabricante> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var fabricantes = new List<Fabricante>();
            while (reader.Read())
            {
                var temObjeto = new Fabricante()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nome = reader["Nome"].ToString(),
                };
                fabricantes.Add(temObjeto);
            }

            reader.Close();
            return fabricantes;
        }
    }
}
