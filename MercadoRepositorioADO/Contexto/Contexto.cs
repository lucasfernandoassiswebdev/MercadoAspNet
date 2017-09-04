using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MercadoRepositorioADO.Contexto
{
    public class Contexto : IDisposable
    {
        private readonly SqlConnection _minhaConexao;

        public Contexto()
        {
            _minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoMercado"].ConnectionString);
            _minhaConexao.Open();
        }

        public SqlCommand ExecutaComando(string procedureName)
        {
            return new SqlCommand
            {
                CommandText = procedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = _minhaConexao
            };
        }

        public void Dispose()
        {
            if (_minhaConexao.State == System.Data.ConnectionState.Open)
                _minhaConexao.Close();
        }
    }
}
