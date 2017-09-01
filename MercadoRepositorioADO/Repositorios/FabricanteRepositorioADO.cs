using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;

namespace MercadoRepositorioADO.Repositorios
{
    public class FabricanteRepositorioADO : IFabricanteRepositorio
    {
        private Contexto.Contexto _contexto;

        private void Insert(Fabricante fabricante)
        {
           using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("InsereFabricante");
                cmd.Parameters.AddWithValue("@Nome", fabricante.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Fabricante fabricante)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("AlteraFabricante");
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
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ExcluiFabricante");
                cmd.Parameters.AddWithValue("@Id", fabricante.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Fabricante> ListarTodos()
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ListaFabricantes");
                var fabricantes = new List<Fabricante>();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        fabricantes.Add(new Fabricante()
                        {
                            Id = reader.ReadAsInt("Id"),
                            Nome = reader.ReadAsString("Nome")
                        });
                    }
                return fabricantes;
            }
        }

        public Fabricante ListarPorId(int id)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("ListaFabricantePorId");
                cmd.Parameters.AddWithValue("@Id", id);
                var fabricante = new Fabricante();
                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return new Fabricante()
                        {
                            Id = reader.ReadAsInt("Id"),
                            Nome = reader.ReadAsString("Nome")
                        };

                return null;
            }
        }

        public int VerificaExistenciaSimilar(Fabricante fabricante)
        {
            using (_contexto = new Contexto.Contexto())
            {
                var cmd = _contexto.ExecutaComando("VerificaFabricanteIgual");
                cmd.Parameters.AddWithValue("@nome", fabricante.Nome);
               
                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return 1;
                       
                return 0;
            }
        }
    }
}
