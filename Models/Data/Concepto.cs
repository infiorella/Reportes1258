using System.ComponentModel.DataAnnotations;

namespace Models;

public partial class Concepto
{
    [Key]
    public int IdConcepto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Tipo { get; set; }


    public string ValidacionReniec(int? ValidacionReniec)
    {
        if (ValidacionReniec == 1)
        {
            return "Si";
        }
        else
        {
            return "No";
        }
    }


    public string IdEstado(int? IdEstado)
    {
        if (IdEstado == 1)
        {
            return "Activo";
        }
        else
        {
            return "Inactivo";
        }
    }
    public string Sexo(string? Dato)
    {
        string Sexo = Dato != null ? Dato.Trim() : "";
        if (Sexo == "H")
        {
            return "Hombre";
        }
        else if (Sexo == "M")
        {
            return "Mujer";
        }
        else
        {
            return "";
        }
    }

    public string SituacionMatricula(string? Dato)
    {
        string SituacionMatricula = Dato != null ? Dato.Trim() : "";
        if (SituacionMatricula == "I")
        {
            return "Ingresante";
        }
        else if (SituacionMatricula == "P")
        {
            return "Pomovido";
        }
        else if (SituacionMatricula == "RE")
        {
            return "Reentrante (EBA)";
        }
        else if (SituacionMatricula == "REI")
        {
            return "Reingresante";
        }
        else
        {
            return "";
        }
    }

    public string RolPersonal(int Rol)
    {
        if (Rol == 1)
        {
            return "Docente a Tiempo Completo";
        }
        else if (Rol == 2)
        {
            return "Docente a Tiempo Parcial";
        }
        else if (Rol == 3)
        {
            return "Directivo";
        }
        else
        {
            return "";
        }
    }
    public string Pais(string? Dato)
    {
        string Pais = Dato != null ? Dato.Trim() : "";
        if (Pais == "P")
        {
            return "Perú";
        }
        else if (Pais == "E")
        {
            return "Ecuador";
        }
        else if (Pais == "C")
        {
            return "CoIombia";
        }
        else if (Pais == "B")
        {
            return "BrasiI";
        }
        else if (Pais == "Bo")
        {
            return "BoIivia";
        }
        else if (Pais == "Ch")
        {
            return "Chile";
        }
        else if (Pais == "OT")
        {
            return "Otro";
        }
        else
        {
            return "";
        }
    }

    public string LenguaMaterna(string? Dato)
    {
        string LenguaMaterna = Dato != null ? Dato.Trim() : "";
        if (LenguaMaterna == "C")
        {
            return "Castellano";
        }
        else if (LenguaMaterna == "Q")
        {
            return "Quechua";
        }
        else if (LenguaMaterna == "AI")
        {
            return "Aimara";
        }
        else if (LenguaMaterna == "OT")
        {
            return "Otra lengua";
        }
        else if (LenguaMaterna == "E")
        {
            return "Lengua extranjera";
        }

        else
        {
            return "";
        }
    }
    public string SegundaLengua(string? Dato)
    {
        string SegundaLengua = Dato != null ? Dato.Trim() : "";
        if (SegundaLengua == "C")
        {
            return "Castellano";
        }
        else if (SegundaLengua == "Q")
        {
            return "Quechua";
        }
        else if (SegundaLengua == "AI")
        {
            return "Aimara";
        }
        else if (SegundaLengua == "OT")
        {
            return "Otra lengua";
        }
        else if (SegundaLengua == "E")
        {
            return "Lengua extranjera";
        }

        else
        {
            return "";
        }
    }

    public string EscolaridadMadre(string? Dato)
    {
        string EscolaridadMadre = Dato != null ? Dato.Trim() : "";
        if (EscolaridadMadre == "SE")
        {
            return "Sin Escolaridad";
        }
        else if (EscolaridadMadre == "P")
        {
            return "Primaria";
        }
        else if (EscolaridadMadre == "S")
        {
            return "Secundaria";
        }
        else if (EscolaridadMadre == "SP")
        {
            return "Superior";
        }
        else
        {
            return "";
        }
    }

    public string TipoDiscapacidad(string? Dato)
    {
        string TipoDiscapacidad = Dato != null ? Dato.Trim().ToUpper() : "";
        if (TipoDiscapacidad == "DI")
        {
            return "Intelectual";
        }
        else if (TipoDiscapacidad == "DF")
        {
            return "Física";
        }
        else if (TipoDiscapacidad == "TEA")
        {
            return "Autista";
        }
        else if (TipoDiscapacidad == "DV")
        {
            return "Visual";
        }
        else if (TipoDiscapacidad == "DA")
        {
            return "Auditiva";
        }
        else if (TipoDiscapacidad == "DV")
        {
            return "Visual";
        }
        else
        {
            return "";
        }
    }

    public string estadoColorNota(string? Nota)
    {
        string nota = Nota != null ? Nota.Trim().ToUpper() : "";
        if (nota == "AD")
        {
            return "badge-success";
        }
        else if (nota == "A")
        {
            return "badge-primary";
        }
        else if (nota == "B")
        {
            return "badge-secondary";
        }
        else if (nota == "C")
        {
            return "badge-warning";
        }
        else
        {
            return "";
        }
    }


    public string TipoEntrega(int? type)
    {
        if (type == 1)
        {
            return "Entrega Material";
        }
        else if (type == 2)
        {
            return "Entrega Notas";
        }
        else if (type == 3)
        {
            return "Entrega Alimentos";
        }
        else
        {
            return "";
        }
    }

    public int valorNota(string? Nota)
    {
        string nota = Nota != null ? Nota.Trim().ToUpper() : "";
        if (nota == "AD")
        {
            return 4;
        }
        else if (nota == "A")
        {
            return 3;
        }
        else if (nota == "B")
        {
            return 2;
        }
        else if (nota == "C")
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


    public string DescripcionNota(double Nota)
    {
        if (Nota > 0 && Nota <= 1)
        {
            return "C";
        }
        else if (Nota > 1 && Nota <= 2)
        {
            return "B";
        }
        else if (Nota > 2 && Nota <= 3)
        {
            return "C";
        }
        else if (Nota > 3 && Nota <= 4)
        {
            return "D";
        }
        {
            return "";
        }
    }


    public string TipoInforme(int? type)
    {
        if (type == 1)
        {
            return "Informe Asistencia";
        }
        else if (type == 2)
        {
            return "Informe Comportamiento";
        }
        else if (type == 3)
        {
            return "Acta de Compromiso";
        }
        else if (type == 4)
        {
            return "Informe Notas";
        }
        else if (type == 5)
        {
            return "Acta Sesión Extraordinaria";
        }
        else if (type == 6)
        {
            return "Acta Sesión Ordinaria";
        }
        else
        {
            return "";
        }
    }

    public string ClasificacionInforme(int? type)
    {
        if (type == 1 || type == 2 || type == 4)
        {
            return "Informe";
        }

        else if (type == 5 || type == 6 || type == 3)
        {
            return "Acta";
        }
        else
        {
            return "";
        }
    }

    public string ClasificacionAi(string? type)
    {
        if (type =="I")
        {
            return "Informe";
        }

        else if (type=="A")
        {
            return "Acta";
        }
        else
        {
            return "";
        }
    }

    public string EstadoInforme(string estado)
    {
        if (estado == "P")
        {
            return "Pendiente";
        }
        else if (estado == "E")
        {
            return "Enviado";
        }
        else if (estado == "A")
        {
            return "Aprobado";
        }
        else if (estado == "R")
        {
            return "Rechazado";
        }
        else
        {
            return "";
        }
    }

    public string EstadoColorInforme(string estado)
    {
        if (estado == "P")
        {
            return "badge-secondary";
        }
        else if (estado == "E")
        {
            return "badge-primary";
        }
        else if (estado == "A")
        {
            return "badge-success";
        }
        else if (estado == "R")
        {
            return "badge-danger";
        }
        else
        {
            return "";
        }
    }

    public string EstadoComportamiento(int? estado)
    {
        if (estado == 1)
        {
            return "Pendiente";
        }
        else if (estado == 2)
        {
            return "Reportado";
        }
        else if (estado == 3)
        {
            return "Solucionado";
        }
        else
        {
            return "";
        }
    }

    public string TipoComportamiento(int tipo)
    {
        if (tipo == 1)
        {
            return "Bullying";
        }
        else if (tipo == 2)
        {
            return "Bulla en clase";
        }
        else if (tipo == 3)
        {
            return "Mal Comportamiento";
        }
        else if (tipo == 4)
        {
            return "Otro";
        }
        else
        {
            return "";
        }
    }

    public string rol(int? type)
    {
        if (type == 3)
        {
            return "Docente";
        }
        else if (type == 2)
        {
            return "Directivo";
        }
        else if (type == 1)
        {
            return "Administrador";
        }
        else
        {
            return "";
        }
    }



    public string MesNombre(int month)
    {
        switch (month)
        {
            case 1:
                return "Enero";
            case 2:
                return "Febrero";
            case 3:
                return "Marzo";
            case 4:
                return "Abril";
            case 5:
                return "Mayo";
            case 6:
                return "Junio";
            case 7:
                return "Julio";
            case 8:
                return "Agosto";
            case 9:
                return "Setiembre";
            case 10:
                return "Octubre";
            case 11:
                return "Noviembre";
            case 12:
                return "Diciembre";
            default:
                return "";
        }
    }

    public string TipoReunion(string type)
    {
        switch (type)
        {
            case "1":
                return "Extraordinario";
            case "2":
                return "Ordinario";            
            default:
                return "Reunión";
        }
    }
}



