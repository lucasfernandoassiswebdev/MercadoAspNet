using System.Collections.Generic;
using MercadoDominio.Entidades;

namespace MercadoAplicacao.DistribuidorApp
{
    public interface IDistribuidorAplicacao
    {
        void Salvar(Distribuidor distribuidor);
        void Excluir(Distribuidor distribuidor);
        IEnumerable<Distribuidor> ListarTodos();
        Distribuidor ListarPorId(int id);
    }
}
