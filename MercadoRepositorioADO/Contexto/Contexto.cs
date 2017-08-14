using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Mercado.RepositorioADO
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

        public SqlDataReader ExecutaComandoComRetorno(string procedureName)
        {
            return new SqlCommand(procedureName, minhaConexao)
            {
                CommandType = CommandType.StoredProcedure
            }.ExecuteReader();
        }

        public void Dispose()
        {
            if (minhaConexao.State == System.Data.ConnectionState.Open)
                minhaConexao.Close();
        }
    }
}
