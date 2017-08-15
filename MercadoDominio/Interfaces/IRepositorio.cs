﻿using System.Collections.Generic;

namespace MercadoDominio.Entidades.Entidades.Contrato
{
    public interface IRepositorio<T> where T : class
    {
        void Salvar(T entidade);

        void Excluir(T entidade);

        IEnumerable<T> ListarTodos();

        T ListarPorId(int Id);
    }
}
