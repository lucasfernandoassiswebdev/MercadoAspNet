using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoDominio.Interfaces
{
    public interface IFabricanteRepositorio
    {
        void Salvar(Fabricante fabricante);
        void Excluir(Fabricante fabricanter);
        IEnumerable<Fabricante> ListarTodos();
        Fabricante ListarPorId(int id);
        int VerificaExistenciaSimilar(Fabricante fabricante);
        int VerificaFabricante(int id);
    }
}
