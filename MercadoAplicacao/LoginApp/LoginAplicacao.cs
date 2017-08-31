using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.LoginApp
{
    public class LoginAplicacao
    {
        private readonly IRepositorio<Login> repositorio;

        public LoginAplicacao(IRepositorio<Login> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Login login)
        {
            repositorio.Salvar(login);
        } 

        public void Excluir(Login login)
        {
            repositorio.Excluir(login);
        }

        public IEnumerable<Login> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Login ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}
