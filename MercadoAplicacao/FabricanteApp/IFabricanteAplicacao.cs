using MercadoDominio.Entidades;
using System.Collections.Generic;

namespace MercadoAplicacao.FabricanteApp
{
    public interface IFabricanteAplicacao
    {
        void Salvar(Fabricante fabricante);
        void Excluir(Fabricante fabricante);
        IEnumerable<Fabricante> ListarTodos();
        Fabricante ListarPorId(int id);
        int VerificaExistenciaSimilar(Fabricante fabricante);
    }
}
