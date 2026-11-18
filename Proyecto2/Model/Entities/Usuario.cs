namespace Proyecto2.Model.Entities
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string email_usuario { get; set; }
        public DateTime fecha_registro { get; set; }
        public bool activo {  get; set; }
        string contrasennia { get; set; }

        
    }
}
