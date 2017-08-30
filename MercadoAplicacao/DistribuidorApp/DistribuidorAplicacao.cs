using MercadoDominio.Entidades;
using MercadoDominio.Entidades.Entidades.Contrato;
using System.Collections.Generic;
using MercadoAplicacao.DistribuidorApp;
using System;

namespace Mercado.Aplicacao.DistribuidorApp
{
    public class DistribuidorAplicacao : IDistribuidorAplicacao
    {
        private readonly IRepositorio<Distribuidor> _repositorio;

        public DistribuidorAplicacao(IRepositorio<Distribuidor> repositorio)
        {
            _repositorio = repositorio;
        }

        public void Salvar(Distribuidor distribuidor)
        {
            _repositorio.Salvar(distribuidor);
        }

        public void Excluir(Distribuidor distribuidor)
        {
            _repositorio.Excluir(distribuidor);
        }

        public IEnumerable<Distribuidor> ListarTodos()
        {
            return _repositorio.ListarTodos();
        }

        public Distribuidor ListarPorId(int id)
        {
            return _repositorio.ListarPorId(id);
        }
    }
}
