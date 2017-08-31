using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;

namespace MercadoRepositorioADO.Repositorios
{
    public class LoginRepositorioADO : IRepositorio<Login>
    {
        private Contexto.Contexto contexto;

        private void Insert(Login login) {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("InsereLogin");
                cmd.Parameters.AddWithValue("@login", login.LoginU);
                cmd.Parameters.AddWithValue("@usuario", login.Usuario);
                cmd.Parameters.AddWithValue("@senha", login.Senha);
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(Login login)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("AlteraLogin");
                cmd.Parameters.AddWithValue("@login", login.LoginU);
                cmd.Parameters.AddWithValue("@senha", login.Senha);
                cmd.Parameters.AddWithValue("@funcionario", login.Usuario);
                cmd.ExecuteNonQuery();
            }
        }

        public void Salvar(Login login)
        {
            if (login.Id > 0)
            {
                Alterar(login);
            } else
            {
                Insert(login);
            }
        }

        public void Excluir(Login login)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ExcluiLogin");
                cmd.Parameters.AddWithValue("@Id", login.Usuario);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Login> ListarTodos()
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("ListarLogins");

                var logins = new List<Login>();

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        logins.Add(new Login()
                        {
                            LoginU = reader.ReadAsString("login"),
                            Usuario = reader.ReadAsInt("usuario"),
                            Senha = reader.ReadAsString("senha"),
                            Funcionario = new Usuario
                            {
                                Nome = reader.ReadAsString("Nome"),
                                Nivel = reader.ReadAsString("Nivel")
                            }
                        });
                return logins;
            }
        }

        public Login ListarPorId(int Id)
        {
            using (contexto = new Contexto.Contexto())
            {
                var cmd = contexto.ExecutaComando("LoginFuncionario");
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return new Login()
                        {
                            LoginU = reader.ReadAsString("login"),
                            Usuario = reader.ReadAsInt("usuario"),
                            Senha = reader.ReadAsString("senha"),
                            Funcionario = new Usuario()
                            {
                                Nome = reader.ReadAsString("Nome")
                            }
                        };
                return null;
            }
        }
    }
}
