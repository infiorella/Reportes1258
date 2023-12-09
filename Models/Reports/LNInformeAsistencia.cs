using Models;

namespace Models.Reports
{
    public class LNInformeAsistencia
    {
        //IDDOCENTE
        public int IdReporte { get; set; }
        public int IdDocente { get; set; }
        public string DocenteNombre { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }

        public int Mes { get; set; }

        public string Comentarios { get; set; }


        public int faltasNoJustificadas { get; set; }
        public int faltasJustificadas { get; set; }
        public int tardanzasNoJustificadas { get; set; }
        public int tardanzasJustificadas { get; set; }

        public Tuple<int, int, int, int> CalculoAsistencia(List<Asistencia> asistencia)
        {
            int faltasNoJustificadas = 0; //J
            int faltasJustificadas = 0; //
            int tardanzasNoJustificadas = 0; //
            int tardanzasJustificadas = 0;

            var alumnos = asistencia.ToList().Where(x => x.Estado.Trim().Equals("A")); //Asistio
            faltasNoJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("F")).Count(); //Faltó
            tardanzasNoJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("T")).Count(); //Tardanza
            faltasJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("J")).Count(); //Falta Justificada
            tardanzasJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("U")).Count(); //Tardanza Justificada

            return Tuple.Create(faltasNoJustificadas, faltasJustificadas, tardanzasNoJustificadas, tardanzasJustificadas);
        }

        public LNInformeAsistencia()
        {

        }

        public LNInformeAsistencia(Informe infor, List<Asistencia> asistencia)
        {
            IdReporte = infor.IdInforme;
            DocenteNombre = infor.IdPersonalNavigation.Nombres + " " + infor.IdPersonalNavigation.Apellidos;
            Grado = infor.IdPersonalNavigation.Grado.ToString();
            Seccion = infor.IdPersonalNavigation.Seccion;
            Mes = infor.Mes == null ? 0 : 1;
            Comentarios = infor.Comentario;

            var calculo = CalculoAsistencia(asistencia);
            faltasNoJustificadas = calculo.Item1; //J
            faltasJustificadas = calculo.Item2; //
            tardanzasNoJustificadas = calculo.Item3; //
            tardanzasJustificadas = calculo.Item4;
        }

    }
}
