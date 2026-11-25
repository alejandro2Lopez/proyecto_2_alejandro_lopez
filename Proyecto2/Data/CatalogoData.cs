using Microsoft.Data.SqlClient;
using Proyecto2.Model.Entities;
using System.Data;

namespace Proyecto2.Data
{
    public class CatalogoData
    {

        private readonly BdConnection bdConnection;

        public CatalogoData(BdConnection bdConnection)
        {
            this.bdConnection = bdConnection;
        }

        public List<Rol> obtenerRoles()
        {

            List<Rol> roles = new List<Rol>();

            using (SqlConnection conexion =
                bdConnection.getConnection())
            {
                string query = @"
                    SELECT
                       *
                    FROM dbo.[vw_getRoles]";

                using (SqlCommand comando =
                    new SqlCommand(query, conexion))
                {
                    comando.CommandType = CommandType.Text;

                    conexion.Open();

                    using (SqlDataReader reader =
                        comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Rol rol = new Rol
                            {
                                Id_rol = Convert.ToInt32(
                                      reader["id_rol"]),

                                nombre_rol =
                                      reader["nombre_rol"].ToString()!
                              ,
                                descripcion_rol = reader["descripción_rol"].ToString()!
                            };

                            roles.Add(rol);
                        }
                    }
                }


                return roles;
            }
        }
    }
}
 
