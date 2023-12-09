using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;

namespace Models.Views;

public partial class AlumnoModel
{
    public int IdAlumno { get; set; }

    public int? Grado { get; set; }

    public string Seccion { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? ValidadoReniec { get; set; }

    public string CodigoEstudiante { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string? Sexo { get; set; }

    public string? SexoConcepto { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? FechaNacimiento { get; set; }

    public string EstadoMatricula { get; set; } = null!;

    public string? SituacionMatricula { get; set; }

    public string? Pais { get; set; }

    public string? PadreVive { get; set; }

    public string? MadreVive { get; set; }

    public string? LenguaMaterna { get; set; }

    public string? SegundaLengua { get; set; }

    public string? TrabajaEstudiante { get; set; }

    public int? HorasLabora { get; set; }

    public string? EscolaridadMadre { get; set; }

    public string? NacimientoRegistrado { get; set; }

    public string? TipoDiscapacidad { get; set; }

    public string? CodigoModular { get; set; }

    public string? NombreRjrd { get; set; }

    public string? IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UserCreacion { get; set; } = null!;

    public string? UserModificacion { get; set; }

    public string PorcentajeLogroAlcanzado { get; set; }
    public string LogroAlcanzado { get; set; }
    public string LogroNoAlcanzado { get; set; }

    public virtual ICollection<AlumnoComportamiento> AlumnoComportamientos { get; set; } = new List<AlumnoComportamiento>();

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();

    public virtual ICollection<Padre> Padres { get; set; } = new List<Padre>();
    public AlumnoModel() { }
    public AlumnoModel Detalles(Alumno al)
    {
        AlumnoModel model = new AlumnoModel();
        Concepto con = new Concepto();
        model.IdAlumno = al.IdAlumno;
        model.Grado = al.Grado;
        model.Seccion = al.Seccion;
        model.TipoDocumento = al.TipoDocumento;
        model.NumeroDocumento = al.NumeroDocumento;

        model.ValidadoReniec = con.ValidacionReniec(al.ValidadoReniec);

        model.CodigoEstudiante = al.CodigoEstudiante;
        model.ApellidoPaterno = al.ApellidoPaterno;
        model.ApellidoMaterno = al.ApellidoMaterno;
        model.Nombres = al.Nombres;
        //
        model.Sexo = al.Sexo;
        model.SexoConcepto = con.Sexo(al.Sexo);

        model.FechaNacimiento = al.FechaNacimiento;
        model.EstadoMatricula = al.EstadoMatricula;

        model.SituacionMatricula = con.SituacionMatricula(al.SituacionMatricula);
        model.Pais = con.Pais(al.Pais);

        model.PadreVive = al.PadreVive;
        model.MadreVive = al.MadreVive;
        //
        model.LenguaMaterna = con.LenguaMaterna(al.LenguaMaterna);
        model.SegundaLengua = con.SegundaLengua(al.SegundaLengua);

        model.TrabajaEstudiante = al.TrabajaEstudiante;
        model.HorasLabora = al.HorasLabora;
        //
        model.EscolaridadMadre = con.EscolaridadMadre(al.EscolaridadMadre);
        model.TipoDiscapacidad = con.TipoDiscapacidad(al.TipoDiscapacidad);

        model.CodigoModular = al.CodigoModular;
        model.NombreRjrd = al.NombreRjrd;
        model.IdEstado = con.IdEstado(al.IdEstado);
        model.FechaCreacion = al.FechaCreacion;
        model.FechaModificacion = al.FechaModificacion;
        model.UserCreacion = al.UserCreacion;
        model.UserModificacion = al.UserModificacion;
        model.Padres = al.Padres;
        return model;
    }


}
