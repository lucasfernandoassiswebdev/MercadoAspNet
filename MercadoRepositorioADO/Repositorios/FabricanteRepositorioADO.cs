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
           using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereFabricante");
                cmd.Parameters.AddWithValue("@Nome", fabricante.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Fabricante fabricante)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("AlteraFabricante");
                cmd.Parameters.AddWithValue("@Nome", fabricante.Nome);
                cmd.Parameters.AddWithValue("@Id", fabricante.Id);
                cmd.ExecuteNonQuery();
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
                var cmd = contexto.ExecutaComando("ExcluiFabricante");
                cmd.Parameters.AddWithValue("@Id", fabricante.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Fabricante> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno("ListaFabricantes");

                var fabricantes = new List<Fabricante>();
                while (retornoDataReader.Read())
                {
                    var temObjeto = new Fabricante()
                    {
                        Id = retornoDataReader.ReadAsInt("Id"),
                        Nome = retornoDataReader.ReadAsString("Nome")
                    };
                    fabricantes.Add(temObjeto);
                }

                retornoDataReader.Close();
                return fabricantes;
            }
        }

        public Fabricante ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno("ListaFabricantePorId");
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
