using Mercado.Dominio;
using Mercado.RepositorioADO;
using MercadoDominio.Entidades;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;

namespace MercadoRepositorioADO.Repositorios
{
    public class LoginRepositorioADO
    {
        private Contexto contexto;

        private void Insert(Login login) {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereLogin");
                cmd.Parameters.AddWithValue("@login", login.login);
                cmd.Parameters.AddWithValue("@usuario", login.usuario);
                cmd.Parameters.AddWithValue("@senha", login.senha);
                cmd.ExecuteNonQuery();
            }
        }

        private void Alterar(Login login)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("AlteraLogin");
                cmd.Parameters.AddWithValue("@login", login.login);
                cmd.Parameters.AddWithValue("@senha", login.senha);
                cmd.Parameters.AddWithValue("@funcionario", login.usuario);
                cmd.ExecuteNonQuery();
            }
        }

        public void Salvar(Login login)
        {
            if (login.usuario > 0)
            {
                Insert(login);
            } else
            {
                Alterar(login);
            }
        }

        public IEnumerable<Login> ListarLogins()
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("ListarLogins");

                var logins = new List<Login>();

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        logins.Add(new Login()
                        {
                            login = reader.ReadAsString("login"),
                            usuario = reader.ReadAsInt("usuario"),
                            senha = reader.ReadAsString("senha"),
                            funcionario = new Usuario {
                                Nome = reader.ReadAsString("Nome")
                            }
                        });
                return logins;
            }
        }

        public Login LoginFuncionario(int id)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("LoginFuncionario");
                cmd.Parameters.AddWithValue("@Id",id);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return new Login()
                        {
                            login = reader.ReadAsString("login"),
                            usuario = reader.ReadAsInt("usuario"),
                            senha = reader.ReadAsString("senha"),
                            funcionario = new Usuario()
                            {
                                Nome = reader.ReadAsString("Nome")
                            }
                        };
                return null;
            }
        }
    }
}
