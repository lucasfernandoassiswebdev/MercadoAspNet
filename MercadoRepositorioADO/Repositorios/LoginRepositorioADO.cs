using Mercado.Dominio;
using Mercado.Dominio.Contrato;
using Mercado.RepositorioADO;
using MercadoDominio.Entidades;
using MercadoRepositorioADO.Extensoes;
using System.Collections.Generic;
using System;

namespace MercadoRepositorioADO.Repositorios
{
    public class LoginRepositorioADO : IRepositorio<Login>
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

        public void Excluir(Login login)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("ExcluiLogin");
                cmd.Parameters.AddWithValue("@Id", login.usuario);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Login> ListarTodos()
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
                            Funcionario = new Usuario
                            {
                                Nome = reader.ReadAsString("Nome")
                            }
                        });
                return logins;
            }
        }

        public Login ListarPorId(int Id)
        {
            using (contexto = new Contexto())
            {
                var cmd = contexto.ExecutaComando("LoginFuncionario");
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return new Login()
                        {
                            login = reader.ReadAsString("login"),
                            usuario = reader.ReadAsInt("usuario"),
                            senha = reader.ReadAsString("senha"),
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
