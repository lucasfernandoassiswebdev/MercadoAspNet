using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.LoginApp
{
    public class LoginAplicacao : ILoginAplicacao
    {
        private readonly ILoginRepositorio _appLogin;

        public LoginAplicacao(ILoginRepositorio login)
        {
            _appLogin = login;
        }

        public void Salvar(Login login)
        {
            _appLogin.Salvar(login);
        } 

        public void Excluir(Login login)
        {
            _appLogin.Excluir(login);
        }

        public IEnumerable<Login> ListarTodos()
        {
            return _appLogin.ListarTodos();
        }

        public Login ListarPorId(int id)
        {
            return _appLogin.ListarPorId(id);
        }

        public int VerificaLogin(Login login)
        {
            return _appLogin.VerificaLogin(login);
        }

        public int VerificaExistenciaSimilar(Login login)
        {
            return _appLogin.VerificaExistenciaSimilar(login);
        }
    }
}
