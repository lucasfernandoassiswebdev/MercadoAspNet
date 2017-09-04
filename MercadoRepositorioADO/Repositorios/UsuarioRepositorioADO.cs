using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;

namespace MercadoRepositorioADO.Repositorios
{
    public class UsuarioRepositorioADO : IUsuarioRepositorio
    {
        private Contexto.Contexto contexto;

        private void Insert(Usuario usuario)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereUsuario");
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Nivel", usuario.Nivel);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Usuario usuario)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("AlteraUsuario");
                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Nivel", usuario.Nivel);
                cmd.ExecuteNonQuery();
            }
        }

        public void Salvar(Usuario usuario)
        {
            if (usuario.Id > 0)
                Alterar(usuario);
            else
                Insert(usuario);
        }

        public void Excluir(int Id)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ExcluirUsuario");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ListaUsuarios");
                var listaUsuarios = new List<Usuario>();

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        listaUsuarios.Add( new Usuario()
                        {
                            Id = reader.ReadAsInt("Id"),
                            Nome = reader.ReadAsString("Nome"),
                            Nivel = reader.ReadAsString("Nivel")
                        });

                return listaUsuarios;
            }
        }

        public Usuario ListarPorId(int id)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ListaUsuarioPorId");
                cmd.Parameters.AddWithValue("@Id",id);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return new Usuario()
                        {
                            Nome = reader.ReadAsString("Nome"),
                            Nivel = reader.ReadAsString("Nivel")
                        };
                return null;
            }
        }

        public int VerificaExistenciaSimilar(Usuario usuario)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("VerificaUsuarioIgual");
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@nivel", usuario.Nivel);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        return 1;

                return 0;
            }
        }
    }
}
