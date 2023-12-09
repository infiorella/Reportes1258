using Models;

namespace Models.Reports
{
    public class LNInformeComportamiento
    {
        //IDDOCENTE
        public int IdInforme {  get; set; }
        public string DocenteNombre { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }

        public DateTime FechaIncidencia { get; set; }

        public string Descripcion { get; set; }
        public string Comportamiento { get; set; }
        public string MedidasCorrectivas { get; set; }
        public string Estado { get; set; }

        public string Comentarios { get; set; }
        /*int comportamiento**/
        //GRADO SECCION
        public ICollection<AlumnoComportamiento> Alumnos { get; set; }


        public LNInformeComportamiento(Comportamiento comp)
        {
            Concepto con = new Concepto();
            IdInforme = comp.IdInforme;
            DocenteNombre = comp.IdInformeNavigation.IdPersonalNavigation.Nombres + " " + comp.IdInformeNavigation.IdPersonalNavigation.Apellidos;
            Grado = comp.IdInformeNavigation.IdPersonalNavigation.Grado.ToString();
            Seccion = comp.IdInformeNavigation.IdPersonalNavigation.Seccion;
            FechaIncidencia = comp.FechaIncidencia;
            Descripcion = comp.Descripcion;
            Comportamiento = con.TipoComportamiento(comp.Tipo);
            MedidasCorrectivas = comp.MedidasCorrectivas;
            Estado = con.EstadoComportamiento(comp.Estado);
            Comentarios = comp.Observacion;
            Alumnos = comp.AlumnoComportamientos;
        }

    }
}
