using System.Collections.Generic;
using Models;
using Models.Views;

namespace Models.Reports
{
    public class LNActaSesionOrdinaria
    {
        //IDDOCENTE
        public int IdInforme {  get; set; }
        public string DocenteNombre { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Trimestre { get; set; }

        public string Agenda { get; set; }

        public int CantidadBajoNivel { get; set; }

        public string Comentarios { get; set; }
        public int NotaDefincion(string cal)
        {
            switch (cal.Trim())
            {
                case "AD": return 4;
                case "A": return 2;
                case "B": return 1;
                case "C": return 0;
                default: return 0;
            }
        }
        public int CalculoBajo(List<NotasModel> notas, int? trimestre)
        {

            //Notas totales del grado y seccion, agrupar por curso
            var alumnos = notas.GroupBy(item => new { item.AreaCurricular, item.IdAlumno })
                                            .Select(
                                                g => new
                                                {
                                                    Value = g.Key,
                                                    Count = g.Count(),
                                                    nota1 = g.Average(p => p.ValorCal1),
                                                    nota2 = g.Average(p => p.ValorCal2),
                                                    nota3 = g.Average(p => p.ValorCal3),
                                                }
                                            ).OrderByDescending(x => x.Count).ToList();

            int Cantidad = 0;
            Concepto con = new Concepto();
            foreach (var item in alumnos)
            {
                switch (trimestre)
                {
                    case 1:
                        if (con.DescripcionNota(item.nota1) == "B" ||
                            con.DescripcionNota(item.nota1) == "C")
                            Cantidad++;
                        break;
                    case 2:
                        if (con.DescripcionNota(item.nota2) == "B" ||
                            con.DescripcionNota(item.nota2) == "C")
                            Cantidad++;
                        break;
                    case 3:
                        if (con.DescripcionNota(item.nota3) == "B" ||
                            con.DescripcionNota(item.nota3) == "C")
                            Cantidad++;
                        break;

                }
            }

            return 0;
        }
        public LNActaSesionOrdinaria(Reunion reu, List<Nota> notas)
        {
            DocenteNombre = reu.IdInformeNavigation.IdPersonalNavigation.Nombres + " " + reu.IdInformeNavigation.IdPersonalNavigation.Apellidos;
            Grado = reu.IdInformeNavigation.IdPersonalNavigation.Grado.ToString();
            Seccion = reu.IdInformeNavigation.IdPersonalNavigation.Seccion;
            Fecha = reu.FechaReunion;
            Trimestre = reu.IdInformeNavigation.Trimestre.ToString();
            Agenda = reu.Agenda;
            List<NotasModel> modelo = new List<NotasModel>();
            foreach (var item in notas)
            {
                NotasModel mo = new NotasModel();
                modelo.Add(mo.Detalles(item));
            }

            CantidadBajoNivel = CalculoBajo(modelo, reu.IdInformeNavigation.Trimestre);
            Comentarios = reu.IdInformeNavigation.Comentario;
        }
    }
}
