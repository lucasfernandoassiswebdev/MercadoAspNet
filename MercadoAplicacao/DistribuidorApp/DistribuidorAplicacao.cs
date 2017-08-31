using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoAplicacao.DistribuidorApp
{
    public class DistribuidorAplicacao : IDistribuidorAplicacao
    {
        private readonly IDistribuidorAplicacao _appDistribuidor;

        public DistribuidorAplicacao(IDistribuidorAplicacao distribuidor)
        {
            _appDistribuidor = distribuidor;
        }

        public void Salvar(Distribuidor distribuidor)
        {
            _appDistribuidor.Salvar(distribuidor);
        }

        public void Excluir(Distribuidor distribuidor)
        {
            _appDistribuidor.Excluir(distribuidor);
        }

        public IEnumerable<Distribuidor> ListarTodos()
        {
            return _appDistribuidor.ListarTodos();
        }

        public Distribuidor ListarPorId(int id)
        {
            return _appDistribuidor.ListarPorId(id);
        }
    }
}
