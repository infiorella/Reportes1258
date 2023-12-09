using Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Data
{
    public class DataUsuario
    {
        public int? IdDocente { get; set; }
        public int? Grado { get; set; }
        public string? Seccion { get; set; }
        public int? Rol { get; set; }


        public DataUsuario(int? IdDocente, int? Grado, string? Seccion, int? Rol)
        {
            this.IdDocente = IdDocente;
            this.Grado = Grado;
            this.Seccion = Seccion;
            this.Rol = Rol;
        }

        public DataUsuario()
        {
        }


    }
}
