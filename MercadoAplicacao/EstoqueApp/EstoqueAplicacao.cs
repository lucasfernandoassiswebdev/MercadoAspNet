using System;
using System.Collections.Generic;
using Mercado.Dominio;
using Mercado.Dominio.Contrato;

namespace Mercado.Aplicacao.EstoqueApp
{
    public class EstoqueAplicacao
    {
        public readonly IRepositorio<Estoque> repositorio;

        public EstoqueAplicacao(IRepositorio<Estoque> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Estoque estoque)
        {
            repositorio.Salvar(estoque);
        }

        public void Excluir(Estoque estoque)
        {
            repositorio.Excluir(estoque);
        }

        public IEnumerable<Estoque> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Estoque ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}
