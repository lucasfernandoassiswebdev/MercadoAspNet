using Mercado.Dominio;
using Mercado.Dominio.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.Aplicacao.UsuarioApp
{
    public class UsuarioAplicacao
    {
        private readonly IRepositorio<Usuario> repositorio;

        public UsuarioAplicacao(IRepositorio<Usuario> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Usuario usuario)
        {
            repositorio.Salvar(usuario);
        }

        public void Excluir(Usuario usuario)
        {
            repositorio.Excluir(usuario);
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Usuario ListarPorId(string id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}
