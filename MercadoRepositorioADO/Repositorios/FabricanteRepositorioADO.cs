using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;

namespace MercadoRepositorioADO.Repositorios
{
    public class FabricanteRepositorioADO : IRepositorio<Fabricante>
    {
        private Contexto.Contexto contexto;

        private void Insert(Fabricante fabricante)
        {
           using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereFabricante");
                cmd.Parameters.AddWithValue("@Nome", fabricante.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Fabricante fabricante)
        {
            using (contexto = new Contexto.Contexto())
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
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ExcluiFabricante");
                cmd.Parameters.AddWithValue("@Id", fabricante.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Fabricante> ListarTodos()
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ListaFabricantes");
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
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ListaFabricantePorId");
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
    }
}
