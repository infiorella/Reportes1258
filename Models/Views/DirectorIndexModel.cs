using System;
using System.Collections.Generic;
using Models;
using static System.Collections.Specialized.BitVector32;

namespace Models.Views;

public partial class DirectorIndexModel
{
    public string Nombres { get; set; }
    public int InformesRecibidos { get; set; }
    public int InformesPendientes { get; set; }
    public int IncidenciasReportadas { get; set; }
    public int ReunionesRealizadas { get; set; }
    public List<int> CantidadCategorias { get; set; }
    public List<int> CantidadTipos { get; set; }
    public List<Informe> Informes { get; set; }


    public DirectorIndexModel Detalles(PersonalEducativo per, List<Informe> reports, List<Comportamiento> behaviors, List<Reunion> sessions)
    {
        DirectorIndexModel model = new DirectorIndexModel();
        Nombres = per.Nombres + " " + per.Apellidos;
        InformesRecibidos = reports.Where(x => x.FechaCreacion.Value.Month == DateTime.Now.Month && x.EstadoInforme == "E").Count();
        InformesPendientes = reports.Where(x => x.FechaCreacion.Value.Month == DateTime.Now.Month && x.EstadoInforme == "P").Count();
        IncidenciasReportadas = behaviors.Where(x => x.FechaIncidencia.Month == DateTime.Now.Month).Count();
        ReunionesRealizadas = sessions.Where(x => x.FechaReunion.Month == DateTime.Now.Month).Count();

        CantidadCategorias = reports.GroupBy(x => x.ClasificacionAi).Select(x => x.Count()).ToList();

        CantidadTipos = reports.GroupBy(x => x.Tipo).Select(x => x.Count()).ToList();
        Informes = reports.Take(5).ToList();
        return model;
    }
}
