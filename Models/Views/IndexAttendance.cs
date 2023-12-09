using Humanizer;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Views;

public partial class IndexAttendance
{

    public int faltasJustificadas { get; set; }
    public int faltasNoJustificadas { get; set; }
    public int tardanzasNoJustificadas { get; set; }
    public int tardanzasJustificadas { get; set; }
    public List<int> daysOfMonth { get; set; }

    public static  Tuple<int, int, int, int> CalculoAsistencia(List<Asistencia> asistencia)
    {
        int faltasNoJustificadas = 0; //J
        int faltasJustificadas = 0; //
        int tardanzasNoJustificadas = 0; //
        int tardanzasJustificadas = 0;

        if (asistencia.Count != 0)
        {
            var alumnos = asistencia.ToList().Where(x => x.Estado.Trim().Equals("A")); //Asistio
            faltasNoJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("F")).Count(); //Faltó
            tardanzasNoJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("T")).Count(); //Tardanza
            faltasJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("J")).Count(); //Falta Justificada
            tardanzasJustificadas = asistencia.Where(x => x.Estado.Trim().Equals("U")).Count(); //Tardanza Justificada
        }
        return Tuple.Create(faltasNoJustificadas, faltasJustificadas, tardanzasNoJustificadas, tardanzasJustificadas);
    }


    public static List<List<string>>? ObtenerTabla(List<Asistencia?> Model, int year, int month)
    {
        List<int> daysOfMonth = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day).Day)
                .ToList();

        List<List<string>> lista = new List<List<string>>();
        if (Model.Count() != 0)
        {
            foreach (var alumno in Model.GroupBy(x => x.IdAlumno))
            {
                string nombre = alumno.First().IdAlumnoNavigation.ApellidoPaterno + " " + alumno.First().IdAlumnoNavigation.ApellidoMaterno + " " + alumno.First().IdAlumnoNavigation.Nombres;
                List<string> estados = new List<string>();
                estados.Add(nombre);
                foreach (var dias in daysOfMonth)
                {
                    Asistencia asis = Model.ToList().Where(x => x.FechaAsistencia.Value.Day == dias).FirstOrDefault();
                    if (asis == null)
                    {
                        estados.Add("A");
                    }
                    else
                    {
                        estados.Add(asis.Estado);
                    }
                }
                lista.Add(estados);
            }
        }
        else
        {
            return null;
        }
        return lista;
    }

    public IndexAttendance Detalles(List<Asistencia>? AsistenciaLista)
    {
        IndexAttendance model = new IndexAttendance();
        Tuple<int, int, int, int> valores = CalculoAsistencia(AsistenciaLista);
        model.faltasJustificadas = valores.Item1;
        model.faltasNoJustificadas = valores.Item2;
        model.tardanzasNoJustificadas = valores.Item3;
        model.tardanzasJustificadas = valores.Item4;

        return model;
    }
}
