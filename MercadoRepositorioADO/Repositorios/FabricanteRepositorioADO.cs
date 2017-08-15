using Mercado.Dominio.Contrato;
using Mercado.Dominio;
using System.Collections.Generic;

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
                var cmd = contexto.ExecutaComandoComRetorno("ListaFabricantePorId");
                cmd.Parameters.AddWithValue("@Id", id);

                var fabricante = new Fabricante();
                while (cmd.Read())
                {
                    var temObjeto = new Fabricante()
                    {
                        Id = cmd.ReadAsInt("Id"),
                        Nome = cmd.ReadAsString("Nome")
                    };

                    fabricante.Add(temObjeto);
                }

                cmd.Close();
                return fabricante;
            }
        }
    }
}
