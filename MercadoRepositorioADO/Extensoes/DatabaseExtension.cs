using System.Data.SqlClient;

namespace MercadoRepositorioADO.Extensoes
{
    public static class DatabaseExtension
    {
        public static int? ReadAsIntNull(this SqlDataReader r, string name)
        {
            var ordinal = r.GetOrdinal(name);
            return r.IsDBNull(ordinal) ? (int?)null : r.GetInt32(ordinal);
        }

        public static int ReadAsInt(this SqlDataReader r, string name)
        {
            return r.GetInt32(r.GetOrdinal(name));
        }

        public static string ReadAsString(this SqlDataReader r, string name)
        {
            return r.GetString(r.GetOrdinal(name));
        }

        public static decimal ReadAsDecimal(this SqlDataReader r, string name)
        {
            return r.GetDecimal(r.GetOrdinal(name));
        }
    }
}
