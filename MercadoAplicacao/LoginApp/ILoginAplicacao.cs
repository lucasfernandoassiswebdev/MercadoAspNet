using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoAplicacao.LoginApp
{
    public interface ILoginAplicacao
    {
        void Salvar(Login login);
        void Excluir(Login login);
        IEnumerable<Login> ListarTodos();
        Login ListarPorId(int id);
        int VerificaLogin(int id);
        int VerificaExistenciaSimilar(Login login);
    }
}
