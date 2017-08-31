using MercadoDominio.Entidades;
using System.Collections.Generic;

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
