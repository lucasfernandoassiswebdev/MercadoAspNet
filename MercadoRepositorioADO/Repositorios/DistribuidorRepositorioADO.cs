using MercadoDominio.Entidades.Contrato;
using MercadoDominio.Entidades;
using System.Collections.Generic;
using MercadoRepositorioADO.Extensoes;

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
                var cmd = contexto.ExecutaComando("ListaDistribuidores");
                var distribuidores = new List<Distribuidor>();

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        distribuidores.Add( new Distribuidor()
                        {
                            Id = reader.ReadAsInt("Id"),
                            Nome = reader.ReadAsString("Nome")
                        });
                    };
                return distribuidores;
            }
        }

        public Distribuidor ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("ListarDistribuidorPorId");
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return new Distribuidor
                        {
                            Id = reader.ReadAsInt("Id"),
                            Nome = reader.ReadAsString("Nome")
                        };

                return null;
            }
        }
    }
}
