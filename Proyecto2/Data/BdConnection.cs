using Microsoft.Data.SqlClient;

namespace Proyecto2.Data
{
    public class BdConnection
    {
        private readonly string StringConnection;

        public BdConnection(IConfiguration configuration)
        {
            StringConnection = configuration
                .GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "No se encontró la cadena de conexión ConexionBD."
                );
        }

        public SqlConnection getConnection()
        {
            return new SqlConnection(StringConnection);
        }
    }
}