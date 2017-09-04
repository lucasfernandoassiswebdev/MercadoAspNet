using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IDistribuidorRepositorio
    {
        void Salvar(Distribuidor distribuidor);
        void Excluir(Distribuidor distribuidor);
        IEnumerable<Distribuidor> ListarTodos();
        Distribuidor ListarPorId(int id);
        int VerificaExistenciaSimilar(Distribuidor distribuidor);
        int VerificaDistribuidor(int idDistribuidor);
    }
}
