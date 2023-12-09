using Models;

namespace Models.Reports
{
    public class LNActaSesionExtraordinaria
    {
        //IDDOCENTE
        public int IdInforme { get; set; }
        public string DocenteNombre { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }

        public DateTime? FechaReunion { get; set; }

        public string Agenda { get; set; }
        public List<string> Acuerdos { get; set; }

        public int? Entrega { get; set; }
        public int? ConformidadPadres { get; set; }

        //SINO
        public int? FirmaronPadres { get; set; }
        public string Comentarios { get; set; }

        public List<string> Padres { get; set; }

        public LNActaSesionExtraordinaria(Reunion reu, List<string> padres)
        {
            IdInforme = (int)reu.IdInforme;
            DocenteNombre = reu.IdInformeNavigation.IdPersonalNavigation.Nombres + " " + reu.IdInformeNavigation.IdPersonalNavigation.Apellidos;
            Grado = reu.IdInformeNavigation.IdPersonalNavigation.Grado.ToString();
            Seccion = reu.IdInformeNavigation.IdPersonalNavigation.Seccion;
            FechaReunion = reu.FechaReunion;
            Agenda = reu.Agenda;
            Entrega = reu.Entrega;
            ConformidadPadres = reu.Conformidad;
            FirmaronPadres = reu.FirmaronPadres;
            Comentarios = reu.IdInformeNavigation.Comentario;
            Padres = padres;
        }

    }
}
