using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        void Salvar(Usuario usuario);
        void Excluir(Usuario usuario);
        IEnumerable<Usuario> ListarTodos();
        Usuario ListarPorId(int id);
    }
}
