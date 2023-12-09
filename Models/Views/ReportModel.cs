using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;

namespace Models.Views;

public partial class ReportModel
{
    public int IdInforme { get; set; }

    public int IdPersonal { get; set; }
    public string PersonalNombre { get; set; }

    public string? Clasificacion { get; set; }

    public string? Tipo { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Link { get; set; } = null!;

    public string? Comentario { get; set; }
    public string? EstadoInforme { get; set; }
    public string? Color { get; set; }
     public string? ComentarioDirector { get; set; }
    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public virtual ICollection<Comportamiento> Comportamientos { get; set; } = new List<Comportamiento>();

    public virtual ICollection<Compromiso> Compromisos { get; set; } = new List<Compromiso>();

    public virtual PersonalEducativo IdPersonalNavigation { get; set; } = null!;

    public virtual ICollection<Reunion> Reunions { get; set; } = new List<Reunion>();
    public ReportModel() { }
    public static List<ReportModel> Details(List<Informe> re)
    {
        List<ReportModel> listReport = new List<ReportModel>();
        foreach (var item in re)
        {
            ReportModel model = new ReportModel();
            Concepto con = new Concepto();
            model.IdInforme = item.IdInforme;
            model.IdPersonal = item.IdPersonal;
            model.Clasificacion = con.ClasificacionInforme(item.Tipo);
            model.Tipo = con.TipoInforme(item.Tipo);
            model.Titulo = item.Titulo;
            model.Descripcion = item.Descripcion;
            model.Link = item.Link;
            model.Comentario = item.Comentario;
            model.EstadoInforme = con.EstadoInforme(item.EstadoInforme);
            model.FechaCreacion = item.FechaCreacion;
            model.FechaModificacion = item.FechaModificacion;
            model.FechaEnvio = item.FechaEnvio;
            model.Color = con.estadoColorNota(item.EstadoInforme);
            model.ComentarioDirector = item.ComentarioDirector;
            listReport.Add(model);
        }
        return listReport;
    }


    public static List<ReportModel> DetailsPrincipal(List<Informe> re)
    {
        List<ReportModel> listReport=new List<ReportModel> ();

        
        foreach(var item in re)
        {
            ReportModel model = new ReportModel();
            Concepto con = new Concepto();

            model.IdInforme = item.IdInforme;
            model.IdPersonal = item.IdPersonal;
            model.Clasificacion = con.ClasificacionInforme(item.Tipo);
            model.Tipo = con.TipoInforme(item.Tipo);
            model.PersonalNombre = "Cecillia Jannet Valenzuela Villajuan";
            model.Titulo = item.Titulo;
            model.Descripcion = item.Descripcion;
            model.Link = item.Link;
            model.Comentario = item.Comentario;
            model.EstadoInforme = item.EstadoInforme;
            model.FechaCreacion = item.FechaCreacion;
            model.FechaModificacion = item.FechaModificacion;
            model.FechaEnvio = item.FechaEnvio;
            model.Color = con.estadoColorNota(item.EstadoInforme);

            listReport.Add(model);
        }

        return listReport;
        
    }
}
