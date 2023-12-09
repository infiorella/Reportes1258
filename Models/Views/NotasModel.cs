using System;
using System.Collections.Generic;
using Models;

namespace Models.Views;

public partial class NotasModel
{
    public int IdNota { get; set; }

    public int? IdAlumno { get; set; }

    public int Grado { get; set; }

    public string Seccion { get; set; } = null!;

    public string AreaCurricular { get; set; } = null!;

    public string? Competencia { get; set; }

    public string Cal1 { get; set; }

    public string Cal2 { get; set; }

    public string Cal3 { get; set; }

    public string CalAnual { get; set; }

    public int ValorCal1 { get; set; }

    public int ValorCal2 { get; set; }

    public int ValorCal3 { get; set; }
    public int ValorCalAnual { get; set; }

    public string? Comentario { get; set; }

    public string IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UserCreacion { get; set; } = null!;

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public string estadoColorCal1 { get; set; }
    public string estadoColorCal2 { get; set; }
    public string estadoColorCal3 { get; set; }
    public string estadoColorCalAnual { get; set; }


    public NotasModel Detalles(Nota not)
    {
        NotasModel model = new NotasModel();
        Concepto con = new Concepto();
        IdNota = not.IdNota;
        IdAlumno = not.IdAlumno;
        Grado = not.Grado;
        Seccion = not.Seccion;
        AreaCurricular = not.AreaCurricular;
        Competencia = not.Competencia;
        Cal1 = not.Cal1;
        Cal2 = not.Cal2;
        Cal3 = not.Cal3;
        CalAnual = not.CalAnual;
        ValorCal1 = con.valorNota(not.Cal1);
        ValorCal2 = con.valorNota(not.Cal2);
        ValorCal3 = con.valorNota(not.Cal3);
        ValorCalAnual = con.valorNota(not.CalAnual);
        Comentario = not.Comentario;
        IdEstado = con.IdEstado(not.IdEstado);
        FechaCreacion = not.FechaCreacion;
        UserCreacion = not.UserCreacion;
        IdAlumnoNavigation = not.IdAlumnoNavigation;
        estadoColorCal1 = con.estadoColorNota(not.Cal1);
        estadoColorCal2 = con.estadoColorNota(not.Cal2);
        estadoColorCal3 = con.estadoColorNota(not.Cal3); ;
        estadoColorCalAnual = con.estadoColorNota(not.CalAnual);

        return model;
    }



}
