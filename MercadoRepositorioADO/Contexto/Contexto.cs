using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MercadoRepositorioADO.Contexto
{
    public class Contexto : IDisposable
    {
        private readonly SqlConnection minhaConexao;

        public Contexto()
        {
            minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoMercado"].ConnectionString);
            minhaConexao.Open();
        }

        public SqlCommand ExecutaComando(string procedureName)
        {
            return new SqlCommand
            {
                CommandText = procedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = minhaConexao
            };
        }

        public void Dispose()
        {
            if (minhaConexao.State == System.Data.ConnectionState.Open)
                minhaConexao.Close();
        }
    }
}
