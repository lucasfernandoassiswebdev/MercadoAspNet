using Mercado.Dominio.Contrato;
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
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereDistribuidor");
                cmd.Parameters.AddWithValue("@Nome", distribuidor.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Distribuidor distribuidor)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("AlteraDistribuidor");
                cmd.Parameters.AddWithValue("@Nome", distribuidor.Nome);
                cmd.Parameters.AddWithValue("@Id", distribuidor.Id);
                cmd.ExecuteNonQuery();
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
                var cmd = contexto.ExecutaComando("ExcluiDistribuidor");
                cmd.Parameters.AddWithValue("@Id", distribuidor.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Distribuidor> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno("ListaDistribuidores");

                var distribuidores = new List<Distribuidor>();
                while (retornoDataReader.Read())
                {
                    var temObjeto = new Distribuidor()
                    {
                        Id = retornoDataReader.ReadAsInt("Id"),
                        Nome = retornoDataReader.ReadAsString("Nome")
                    };
                   distribuidores.Add(temObjeto);
                 }
                
                retornoDataReader.Close();

                return distribuidores;
            }
        }

        public Distribuidor ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComandoComRetorno("ListarDistribuidorPorId");
                cmd.Parameters.AddWithValue("@Id", id);

                var distribuidor = new Distribuidor();
                while (cmd.Read())
                {
                    var temObjeto = new Distribuidor()
                    {
                        Id = cmd.ReadAsInt("Id"),
                        Nome = cmd.ReadAsString("Nome")
                    };
                    distribuidor.Add(temObjeto);
                }

                cmd.Close();
                return distribuidor;
            }
        }
    }
}
