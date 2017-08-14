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
            strQuery += " INSERT INTO DBFabricantes(Nome)" +
                          $" VALUES('{fabricante.Nome}')";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Fabricante fabricante)
        {
            var strQuery = "";
            strQuery += "UPDATE DBFabricantes SET " + 
                         $" Nome = '{fabricante.Nome}'" + 
                         $" WHERE Id = {fabricante.Id}";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Fabricante fabricante)
        {
            if (fabricante.Id > 0)
                Alterar(fabricante);
            else
                Insert(fabricante);
        }

        public void Excluir(Fabricante fabricante)
        {
            using (contexto = new Contexto())
            {
                var strQuery = $"DELETE FROM DBFabricantes WHERE Id = '{fabricante.Id}'";
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

        public Fabricante ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = $" SELECT * FROM DBFabricantes WHERE Id = {id}";
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
                    Id = reader.ReadAsInt("Id"),
                    Nome = reader.ReadAsString("Nome")
                };
                fabricantes.Add(temObjeto);
            }

            reader.Close();
            return fabricantes;
        }
    }
}
