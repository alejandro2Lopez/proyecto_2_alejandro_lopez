using Microsoft.Data.SqlClient;
using Proyecto2.Model.Entities;
using System.Data;

namespace Proyecto2.Data
{
    public class ProyectoData
    {

        private readonly BdConnection bdConnection;

        public ProyectoData(BdConnection bdConnection)
        {
            this.bdConnection = bdConnection;
        }
        public List<Proyecto> obtenerProyectos()
        {
            List<Proyecto> proyectos = new List<Proyecto>();

            using (SqlConnection conexion = bdConnection.getConnection())
            {
                string query = @"
            SELECT *
            FROM dbo.[vw_getProyectos]
        ";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.CommandType = CommandType.Text;

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Proyecto proyecto = new Proyecto
                            {
                                id_proyecto = Convert.ToInt32(
                                    reader["id_proyecto"]),

                                nombre_proyecto =
                                    reader["nombre_proyecto"].ToString()!,

                                fecha_inicio = Convert.ToDateTime(
                                    reader["fecha_inicio"]),

                                fecha_fin_estimada =
                                    reader["fecha_fin_estimada"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["fecha_fin_estimada"]),

                                descripcion_proyecto =
                                    reader["descripcion_proyecto"].ToString()!,

                                fk_id_responsable = Convert.ToInt32(
                                    reader["fk_id_responsable"]),

                                fecha_creacion = Convert.ToDateTime(
                                    reader["fecha_creacion"]),

                                responsable =
                                    reader["responsable"].ToString()!,

                                nombre_estado =
                                    reader["nombre_estado"].ToString()!,

                                entidad_aplicacion =
                                    reader["entidad_aplicacion"].ToString()!,

                                fk_id_estado = Convert.ToInt32(
                                    reader["fk_id_estado"])
                            };

                            proyectos.Add(proyecto);
                        }
                    }
                }

                return proyectos;
            }
        }
        public bool InsertarProyecto(Proyecto proyecto)
        {
            using (SqlConnection conexion = bdConnection.getConnection())
            using (SqlCommand comando =
                new SqlCommand("dbo.[sp_InsertarProyecto]", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@nombre_proyecto",
                    SqlDbType.NVarChar, 50
                ).Value = proyecto.nombre_proyecto;

                comando.Parameters.Add(
                    "@descripcion_proyecto",
                    SqlDbType.NVarChar, -1
                ).Value = proyecto.descripcion_proyecto;

                comando.Parameters.Add(
                    "@fecha_inicio",
                    SqlDbType.DateTime
                ).Value = proyecto.fecha_inicio;

                comando.Parameters.Add(
                    "@fecha_fin_estimada",
                    SqlDbType.DateTime
                ).Value = (object?)proyecto.fecha_fin_estimada ?? DBNull.Value;

                comando.Parameters.Add(
                    "@id_estado",
                    SqlDbType.TinyInt
                ).Value = proyecto.fk_id_estado;

                comando.Parameters.Add(
                    "@id_responsable",
                    SqlDbType.Int
                ).Value = proyecto.fk_id_responsable;

                conexion.Open();

                comando.ExecuteNonQuery();

                return true;
            }
        }
        public bool ActualizarProyecto(Proyecto proyecto)
        {
            using (SqlConnection conexion = bdConnection.getConnection())
            using (SqlCommand comando =
                new SqlCommand("dbo.[sp_ActualizarProyecto]", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@id_Proyecto",
                    SqlDbType.Int
                ).Value = proyecto.id_proyecto;

                comando.Parameters.Add(
                    "@nombre_proyecto",
                    SqlDbType.NVarChar, 50
                ).Value = proyecto.nombre_proyecto;

                comando.Parameters.Add(
                    "@descripcion_proyecto",
                    SqlDbType.NVarChar, -1
                ).Value = proyecto.descripcion_proyecto;

                comando.Parameters.Add(
                    "@fecha_inicio",
                    SqlDbType.DateTime
                ).Value = proyecto.fecha_inicio;

                comando.Parameters.Add(
                    "@fecha_fin_estimada",
                    SqlDbType.DateTime
                ).Value = (object?)proyecto.fecha_fin_estimada ?? DBNull.Value;

                comando.Parameters.Add(
                    "@id_estado",
                    SqlDbType.TinyInt
                ).Value = proyecto.fk_id_estado;

                comando.Parameters.Add(
                    "@id_responsable",
                    SqlDbType.Int
                ).Value = proyecto.fk_id_responsable;

              

                conexion.Open();

                comando.ExecuteNonQuery();

                return true;
            }
        }
        public bool EliminarProyecto(int id_proyecto)
        {
            using (SqlConnection conexion = bdConnection.getConnection())
            using (SqlCommand comando =
                new SqlCommand("dbo.[sp_EliminarProyecto]", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add(
                    "@id_Proyecto",
                    SqlDbType.Int
                ).Value = id_proyecto;

                conexion.Open();

                comando.ExecuteNonQuery();

                return true;
            }
        }
    }

}
