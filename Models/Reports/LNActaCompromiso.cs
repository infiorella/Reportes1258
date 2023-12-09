namespace Models.Reports
{
    public class LNActaCompromiso
    {
        //IDDOCENTE
        public int IdInforme { get; set; }
        public string DocenteNombre { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }
        public DateTime FechaCompromiso { get; set; }

        /*int comportamiento**/
        //Acuerdos
        public string Compromisos { get; set; }

        public string Comentarios { get; set; }

        public LNActaCompromiso(Compromiso comp)
        {
            IdInforme = (int)comp.IdInforme;
            DocenteNombre = comp.IdInformeNavigation.IdPersonalNavigation.Nombres + " " + comp.IdInformeNavigation.IdPersonalNavigation.Apellidos;
            Grado = comp.IdInformeNavigation.IdPersonalNavigation.Grado.ToString();
            Seccion = comp.IdInformeNavigation.IdPersonalNavigation.Seccion;
            FechaCompromiso = comp.FechaCompromiso;
            Compromisos = comp.Compromisos;
            Comentarios = comp.IdInformeNavigation.Comentario;
        }

    }
}
