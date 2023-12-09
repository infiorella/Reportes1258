using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;
using NuGet.RuntimeModel;
using static System.Collections.Specialized.BitVector32;

namespace Models.Views;

public partial class DocenteIndexModel
{
    public string Nombres { get; set; }
    public int InformesCreados { get; set; }
    public int InformesAprobados { get; set; }
    public int IncidenciasReportadas { get; set; }
    public int ReunionesRealizadas { get; set; }

    public List<IncidenciasIndexModel>? IncidenciasAlumnos { get; set; }
    public List<ReunionesIndexModel>? Reuniones { get; set; }
    public List<ReportesIndexModel>? Informes { get; set; }

    public List<IncidenciasIndexModel> Incidencias(List<Comportamiento> comp)
    {
        List<IncidenciasIndexModel> lista = new List<IncidenciasIndexModel>();
        if (comp != null)
        {
            foreach (var item in comp)
            {
                Concepto con = new Concepto();
                IncidenciasIndexModel inc = new IncidenciasIndexModel();
                inc.IdComportamiento = item.IdComportamiento;
                inc.FechaIncidencia = item.FechaIncidencia;
                inc.Descripcion = item.Descripcion;
                inc.Tipo = con.TipoComportamiento(item.Tipo);
                inc.Estado = con.EstadoComportamiento(item.Estado);
                inc.IdInforme = item.IdInforme;

                lista.Add(inc);
            }
            return lista;
        }
        return null;
    }

    public List<ReunionesIndexModel> Reunions(List<Reunion> reu)
    {
        List<ReunionesIndexModel> lista = new List<ReunionesIndexModel>();
        if (reu != null)
        {
            foreach (var item in reu)
            {
                Concepto con = new Concepto();
                ReunionesIndexModel inc = new ReunionesIndexModel();
                inc.IdReunion = item.IdReunion;
                inc.TipoReunion = con.TipoReunion(item.TipoReunion);
                inc.FechaReunion = item.FechaReunion;
                inc.Agenda = item.Agenda;
                inc.Acuerdos = item.Acuerdos;
                inc.Entrega = con.TipoEntrega(item.Entrega);
                inc.IdInforme = item.IdInforme;

                lista.Add(inc);
            }
            return lista;
        }
        return null;
    }


    public List<ReportesIndexModel> Reportes(List<Informe> inf)
    {
        List<ReportesIndexModel> lista = new List<ReportesIndexModel>();
        if (inf != null)
        {
            foreach (var item in inf)
            {
                Concepto con = new Concepto();
                ReportesIndexModel inc = new ReportesIndexModel();
                inc.IdInforme = item.IdInforme;
                inc.IdPersonal = item.IdPersonal;
                inc.ClasificacionAi = con.ClasificacionAi(item.ClasificacionAi);
                inc.Tipo = con.TipoInforme(item.Tipo);
                inc.Titulo = item.Titulo;
                inc.Descripcion = item.Descripcion;
                inc.EstadoInforme = con.EstadoInforme(item.EstadoInforme);
                inc.FechaCreacion = item.FechaCreacion;
                lista.Add(inc);
            }
            return lista;
        }
        return null;
    }

    public DocenteIndexModel Detalles(PersonalEducativo per, List<Informe> reports, List<Comportamiento> behaviors, List<Reunion> sessions)
    {
        DocenteIndexModel model = new DocenteIndexModel();
        Nombres = per.Nombres + " " + per.Apellidos;
        InformesCreados = reports.Where(x => x.FechaCreacion.Value.Month == DateTime.Now.Month && x.EstadoInforme == "P").Count();
        InformesCreados = reports.Where(x => x.FechaCreacion.Value.Month == DateTime.Now.Month && x.EstadoInforme == "A").Count();
        IncidenciasReportadas = behaviors.Where(x => x.FechaIncidencia.Month == DateTime.Now.Month).Count();
        ReunionesRealizadas = sessions.Where(x => x.FechaReunion.Month == DateTime.Now.Month).Count();

        IncidenciasAlumnos = Incidencias(behaviors.Take(5).ToList());
        Reuniones = Reunions(sessions.Take(5).ToList());
        Informes = Reportes(reports.Take(5).ToList());

        return model;
    }

    public class IncidenciasIndexModel
    {
        public int IdComportamiento { get; set; }
        public DateTime FechaIncidencia { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Tipo { get; set; }

        public string? Estado { get; set; }

        public int IdInforme { get; set; }

    }


    public class ReunionesIndexModel
    {
        public int IdReunion { get; set; }

        public string TipoReunion { get; set; }
        public DateTime FechaReunion { get; set; }

        public string? Agenda { get; set; }

        public string? Acuerdos { get; set; }

        public string? Entrega { get; set; }

        public int? IdInforme { get; set; }
    }


    public class ReportesIndexModel
    {
        public int IdInforme { get; set; }

        public int IdPersonal { get; set; }

        public string? ClasificacionAi { get; set; }

        public string? Tipo { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }
       
        public string? EstadoInforme { get; set; }
        public DateTime? FechaCreacion { get; set; }

    }
}
