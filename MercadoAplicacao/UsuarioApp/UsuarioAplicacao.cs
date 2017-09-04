using MercadoDominio.Entidades;
using MercadoDominio.Interfaces;
using System.Collections.Generic;

namespace MercadoAplicacao.UsuarioApp
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        private readonly IUsuarioRepositorio _appUsuario;

        public UsuarioAplicacao(IUsuarioRepositorio usuario)
        {
            _appUsuario = usuario;
        }

        public void Salvar(Usuario usuario)
        {
            _appUsuario.Salvar(usuario);
        }

        public void Excluir(int Id)
        {
            _appUsuario.Excluir(Id);
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return _appUsuario.ListarTodos();
        }

        public Usuario ListarPorId(int id)
        {
            return _appUsuario.ListarPorId(id);
        }

        public int VerificaExistenciaSimilar(Usuario usuario)
        {
            return _appUsuario.VerificaExistenciaSimilar(usuario);
        }
    }
}
