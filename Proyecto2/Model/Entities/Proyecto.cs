namespace Proyecto2.Model.Entities
{
    public class Proyecto
    {
      
            public int id_proyecto { get; set; }

            public string nombre_proyecto { get; set; }

            public DateTime fecha_inicio { get; set; }

            public DateTime? fecha_fin_estimada { get; set; }

            public string descripcion_proyecto { get; set; }

            public int fk_id_responsable { get; set; }

            public DateTime fecha_creacion { get; set; }

            public string? responsable { get; set; }

            public string? nombre_estado { get; set; }

            public string? entidad_aplicacion { get; set; }

            public int fk_id_estado { get; set; }

        }
    }
 
