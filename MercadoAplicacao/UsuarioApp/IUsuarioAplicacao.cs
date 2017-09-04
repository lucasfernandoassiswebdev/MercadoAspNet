using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoAplicacao.UsuarioApp
{
    public interface IUsuarioAplicacao
    {
        void Salvar(Usuario usuario);
        void Excluir(int Id);
        IEnumerable<Usuario> ListarTodos();
        Usuario ListarPorId(int id);
        int VerificaExistenciaSimilar(Usuario usuario);
    }
}
