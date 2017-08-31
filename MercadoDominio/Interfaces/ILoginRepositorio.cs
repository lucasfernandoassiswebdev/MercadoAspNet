using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface ILoginRepositorio
    {
        void Salvar(Login login);
        void Excluir(Login login);
        IEnumerable<Login> ListarTodos();
        Login ListarPorId(int id);
    }
}
