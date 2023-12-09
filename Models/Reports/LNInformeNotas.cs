using Models;
using Models.Views;
using static NuGet.Packaging.PackagingConstants;

namespace Models.Reports
{
    public class LNInformeNotas
    {
        //IDDOCENTE
        public int IdInforme {  get; set; }
        public int IdPersonal { get; set; }
        public string Grado { get; set; }
        public string DocenteNombre { get; set; }

        public string Seccion { get; set; }

        public int? Trimestre { get; set; }
        public string Comentarios { get; set; }

        public int CantidadAD { get; set; }
        public int CantidadA { get; set; }

        public int CantidadB { get; set; }
        public int CantidadC { get; set; }

        public Tuple<int, int, int, int> CalculoProgreso(List<NotasModel> notas, int? trimestre)
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

            int CantidadAD = 0;
            int CantidadA = 0;
            int CantidadB = 0;
            int CantidadC = 0;

            Concepto con = new Concepto();
            foreach (var item in alumnos)
            {
                switch (trimestre)
                {
                    case 1:
                        if (con.DescripcionNota(item.nota1) == "AD") CantidadAD++;
                        else if (con.DescripcionNota(item.nota1) == "A") CantidadA++;
                        else if (con.DescripcionNota(item.nota1) == "B") CantidadB++;
                        else if (con.DescripcionNota(item.nota1) == "C") CantidadC++;
                        break;
                    case 2:
                        if (con.DescripcionNota(item.nota2) == "AD") CantidadAD++;
                        else if (con.DescripcionNota(item.nota2) == "A") CantidadA++;
                        else if (con.DescripcionNota(item.nota2) == "B") CantidadB++;
                        else if (con.DescripcionNota(item.nota2) == "C") CantidadC++;
                        break;
                    case 3:
                        if (con.DescripcionNota(item.nota3) == "AD") CantidadAD++;
                        else if (con.DescripcionNota(item.nota3) == "A") CantidadA++;
                        else if (con.DescripcionNota(item.nota3) == "B") CantidadB++;
                        else if (con.DescripcionNota(item.nota3) == "C") CantidadC++;
                        break;

                }
            }

            return Tuple.Create(CantidadAD, CantidadA, CantidadB, CantidadC);
        }

        public LNInformeNotas(Informe infor, List<Nota> notas)
        {
            DocenteNombre = infor.IdPersonalNavigation.Nombres + " " + infor.IdPersonalNavigation.Apellidos;
            Grado = infor.IdPersonalNavigation.Grado.ToString();
            Seccion = infor.IdPersonalNavigation.Seccion;
            Trimestre = infor.Trimestre;
            Comentarios = infor.Comentario;

            List<NotasModel> modelo = new List<NotasModel>();
            foreach (var item in notas)
            {
                NotasModel mo = new NotasModel();
                modelo.Add(mo.Detalles(item));
            }

            var calculo = CalculoProgreso(modelo, infor.Trimestre);
            CantidadAD = calculo.Item1;
            CantidadA = calculo.Item2;
            CantidadB = calculo.Item3;
            CantidadC = calculo.Item4;
        }


    }
}
