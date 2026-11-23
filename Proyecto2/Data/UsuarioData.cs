using Microsoft.Data.SqlClient;
using Proyecto2.Model.Entities;
using System.Data;
using System.Data.Common;

namespace Proyecto2.Data
{
    public class UsuarioData
    {
        private readonly BdConnection bdConnection;
      
        public UsuarioData(BdConnection bdConnection)
        {
            this.bdConnection = bdConnection;
        }

        public UsuarioData () { 
        
        }
    
        public Usuario? ValidarUsuario(string email, string password)
        {
            using (SqlConnection conexion = bdConnection.getConnection())
            using (SqlCommand comando =
                new SqlCommand("dbo.[sp_ValidarUsuario]", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@email_usuario",
                    SqlDbType.NVarChar, 100
                ).Value = email;

                comando.Parameters.Add(
                    "@contrasenia_usuario",
                    SqlDbType.NVarChar, 100
                ).Value = password;

                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            id_usuario = Convert.ToInt32(
                                reader["id_usuario"]),

                            nombre_usuario =
                                reader["nombre_usuario"].ToString()!,

                            email_usuario =
                                reader["email_usuario"].ToString()!,

                            rol = new Rol
                            {
                                Id_rol = Convert.ToByte(
                                    reader["fk_id_rol"]),
                                nombre_rol = reader["nombre_rol"].ToString()!
                            },

                            activo = Convert.ToBoolean(
                                reader["activo"])
                        };
                    }
                }
            }

            return null;
        }
    }
    }
