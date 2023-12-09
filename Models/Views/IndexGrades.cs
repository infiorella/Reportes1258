using Humanizer;
using Models;
using Models.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Views;

public partial class IndexGrades
{
    public string Alumno { get; set; }
    public string Competencias { get; set; }
    public string Trimestre1 { get; set; }
    public string Trimestre2 { get; set; }
    public string Trimestre3 { get; set; }
    public string Comentario { get; set; }

    public static Tuple<int, int, double> CalculoProgreso(List<Nota> Nota)
    {
        List<NotasModel> notas = new List<NotasModel>();
        foreach (var item in Nota)
        {
            NotasModel mo = new NotasModel();
            mo.Detalles(item);
            notas.Add(mo);
        }

        if (notas.Count == 0)
        {
            return Tuple.Create(0, 0, 0.0);
        }
        else
        {
            var alumnos = notas.GroupBy(x => x.IdAlumno)
                                            .Select(
                                                g => new
                                                {
                                                    Value = g.Key,
                                                    Count = g.Count(),
                                                    nota1 = g.Average(p => p.ValorCal1),
                                                    nota2 = g.Average(p => p.ValorCal2),
                                                    nota3 = g.Average(p => p.ValorCal3)
                                                }
                                            ).OrderByDescending(x => x.Count).ToList();

            int desaprobados = 0;
            int aprobados = 0;
            double logroalcanzado = 0;
            foreach (var item in alumnos)
            {
                double suma = item.nota1 + item.nota2 + item.nota3;
                double divisor = item.nota1 != 0 ? item.nota1 : 0 + item.nota2 != 0 ? item.nota2 : 0 + item.nota3 != 0 ? item.nota3 : 0;

                double valornota = suma / divisor;

                if (valornota >= 2)
                {
                    aprobados++;
                }
                else
                {
                    desaprobados++;
                }
            }
            logroalcanzado = aprobados * 100 / alumnos.Count();

            return Tuple.Create(desaprobados, aprobados, logroalcanzado);
        }
    }


    public static Tuple<int, int, double> CalculoProgresoEstudiante(List<Nota> Nota)
    {
        List<NotasModel> notas = new List<NotasModel>();
        foreach (var item in Nota)
        {
            NotasModel mo = new NotasModel();
            mo.Detalles(item);
            notas.Add(mo);
        }

        if (notas.Count == 0)
        {
            return Tuple.Create(0, 0, 0.0);
        }
        else
        {
            var alumnos = notas.GroupBy(x => x.AreaCurricular)
                                            .Select(
                                                g => new
                                                {
                                                    Value = g.Key,
                                                    Count = g.Count(),
                                                    nota1 = g.Average(p => p.ValorCal1),
                                                    nota2 = g.Average(p => p.ValorCal2),
                                                    nota3 = g.Average(p => p.ValorCal3)
                                                }
                                            ).OrderByDescending(x => x.Count).ToList();

            int desaprobados = 0;
            int aprobados = 0;
            double logroalcanzado = 0;
            foreach (var item in alumnos)
            {
                double suma = item.nota1 + item.nota2 + item.nota3;
                double divisor = item.nota1 != 0 ? item.nota1 : 0 + item.nota2 != 0 ? item.nota2 : 0 + item.nota3 != 0 ? item.nota3 : 0;

                double valornota = suma / divisor;

                if (valornota > 2)
                {
                    aprobados++;
                }
                else
                {
                    desaprobados++;
                }
            }
            logroalcanzado = aprobados * 100 / alumnos.Count();

            return Tuple.Create(desaprobados, aprobados, logroalcanzado);
        }
    }
    public static IndexGrades ObtenerRegistros(Nota notas)
    {
        IndexGrades model = new IndexGrades();
        model.Alumno = notas.IdAlumnoNavigation.Nombres + " " + notas.IdAlumnoNavigation.ApellidoPaterno;
        model.Competencias = notas.Competencia.Trim();
        model.Trimestre1 = (notas.Cal1==null)? "": notas.Cal1.Trim();
        model.Trimestre2 = (notas.Cal2 == null) ? "" : notas.Cal2.Trim();
        model.Trimestre3 = (notas.Cal3 == null) ? "" : notas.Cal3.Trim();
        model.Comentario = (notas.Comentario == null) ? "" : notas.Comentario.Trim();

        return model;

    }

    public static List<IndexGrades> ObtenerTabla(List<Nota?> notas)
    {
        List<IndexGrades> lista= new List<IndexGrades> ();
        if (notas != null){
            foreach (var item in notas)
            {
                if (item != null)
                {
                    lista.Add(ObtenerRegistros(item));
                }
            }
        }

        return lista;

    }


}
