using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;

namespace MercadoRepositorioADO.Repositorios
{
    public class DistribuidorRepositorioADO : IDistribuidorRepositorio
    {
        private Contexto.Contexto _contexto;

        private void Insert(Distribuidor distribuidor)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("InsereDistribuidor");
                cmd.Parameters.AddWithValue("@Nome", distribuidor.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Distribuidor distribuidor)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("AlteraDistribuidor");
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
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ExcluiDistribuidor");
                cmd.Parameters.AddWithValue("@Id", distribuidor.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Distribuidor> ListarTodos()
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ListaDistribuidores");
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
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ListaDistribuidorPorId");
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

        public int VerificaExistenciaSimilar(Distribuidor distribuidor)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("VerificaDistribuidorIgual");
                cmd.Parameters.AddWithValue("@nome", distribuidor.Nome);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return 1;

                return 0;
            }
        }
    }
}
