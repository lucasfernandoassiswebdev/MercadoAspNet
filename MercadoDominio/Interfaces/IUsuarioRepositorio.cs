using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        void Salvar(Usuario usuario);
        void Excluir(int Id);
        IEnumerable<Usuario> ListarTodos();
        Usuario ListarPorId(int id);
        int VerificaExistenciaSimilar(Usuario usuario);
        IEnumerable<Usuario> ListarUsuariosSemLogin();
    }
}
