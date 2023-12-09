using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colegio1258.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumno",
                columns: table => new
                {
                    IdAlumno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grado = table.Column<int>(type: "int", nullable: true),
                    Seccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidadoReniec = table.Column<int>(type: "int", nullable: true),
                    CodigoEstudiante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoMatricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SituacionMatricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PadreVive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MadreVive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LenguaMaterna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundaLengua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrabajaEstudiante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorasLabora = table.Column<int>(type: "int", nullable: true),
                    EscolaridadMadre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDiscapacidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoModular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreRjrd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumno", x => x.IdAlumno);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    IdAsistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAsistencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdAlumno = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAlumnoNavigationIdAlumno = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.IdAsistencia);
                    table.ForeignKey(
                        name: "FK_Asistencia_Alumno_IdAlumnoNavigationIdAlumno",
                        column: x => x.IdAlumnoNavigationIdAlumno,
                        principalTable: "Alumno",
                        principalColumn: "IdAlumno");
                });

            migrationBuilder.CreateTable(
                name: "Nota",
                columns: table => new
                {
                    IdNota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlumno = table.Column<int>(type: "int", nullable: true),
                    Grado = table.Column<int>(type: "int", nullable: false),
                    Seccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaCurricular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Competencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cal1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cal2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cal3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalAnual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAlumnoNavigationIdAlumno = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nota", x => x.IdNota);
                    table.ForeignKey(
                        name: "FK_Nota_Alumno_IdAlumnoNavigationIdAlumno",
                        column: x => x.IdAlumnoNavigationIdAlumno,
                        principalTable: "Alumno",
                        principalColumn: "IdAlumno");
                });

            migrationBuilder.CreateTable(
                name: "Padre",
                columns: table => new
                {
                    IdPadre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parentesco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidadoReniec = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAlumno = table.Column<int>(type: "int", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAlumnoNavigationIdAlumno = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Padre", x => x.IdPadre);
                    table.ForeignKey(
                        name: "FK_Padre_Alumno_IdAlumnoNavigationIdAlumno",
                        column: x => x.IdAlumnoNavigationIdAlumno,
                        principalTable: "Alumno",
                        principalColumn: "IdAlumno");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalEducativo",
                columns: table => new
                {
                    IdPersonal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grado = table.Column<int>(type: "int", nullable: true),
                    Seccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rol = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuarioNavigationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalEducativo", x => x.IdPersonal);
                    table.ForeignKey(
                        name: "FK_PersonalEducativo_Users_IdUsuarioNavigationId",
                        column: x => x.IdUsuarioNavigationId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Informe",
                columns: table => new
                {
                    IdInforme = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonal = table.Column<int>(type: "int", nullable: false),
                    ClasificacionAi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mes = table.Column<int>(type: "int", nullable: true),
                    Trimestre = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoInforme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ComentarioDirector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPersonalNavigationIdPersonal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informe", x => x.IdInforme);
                    table.ForeignKey(
                        name: "FK_Informe_PersonalEducativo_IdPersonalNavigationIdPersonal",
                        column: x => x.IdPersonalNavigationIdPersonal,
                        principalTable: "PersonalEducativo",
                        principalColumn: "IdPersonal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comportamiento",
                columns: table => new
                {
                    IdComportamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaIncidencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedidasCorrectivas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdInforme = table.Column<int>(type: "int", nullable: false),
                    IdInformeNavigationIdInforme = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comportamiento", x => x.IdComportamiento);
                    table.ForeignKey(
                        name: "FK_Comportamiento_Informe_IdInformeNavigationIdInforme",
                        column: x => x.IdInformeNavigationIdInforme,
                        principalTable: "Informe",
                        principalColumn: "IdInforme",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compromiso",
                columns: table => new
                {
                    IdCompromiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompromiso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Compromisos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdInforme = table.Column<int>(type: "int", nullable: true),
                    IdInformeNavigationIdInforme = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compromiso", x => x.IdCompromiso);
                    table.ForeignKey(
                        name: "FK_Compromiso_Informe_IdInformeNavigationIdInforme",
                        column: x => x.IdInformeNavigationIdInforme,
                        principalTable: "Informe",
                        principalColumn: "IdInforme");
                });

            migrationBuilder.CreateTable(
                name: "Reunion",
                columns: table => new
                {
                    IdReunion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoReunion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaReunion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acuerdos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entrega = table.Column<int>(type: "int", nullable: true),
                    Conformidad = table.Column<int>(type: "int", nullable: true),
                    FirmaronPadres = table.Column<int>(type: "int", nullable: true),
                    IdInforme = table.Column<int>(type: "int", nullable: true),
                    IdInformeNavigationIdInforme = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reunion", x => x.IdReunion);
                    table.ForeignKey(
                        name: "FK_Reunion_Informe_IdInformeNavigationIdInforme",
                        column: x => x.IdInformeNavigationIdInforme,
                        principalTable: "Informe",
                        principalColumn: "IdInforme");
                });

            migrationBuilder.CreateTable(
                name: "AlumnoComportamiento",
                columns: table => new
                {
                    IdAlumnoComportamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdComportamiento = table.Column<int>(type: "int", nullable: false),
                    IdAlumno = table.Column<int>(type: "int", nullable: false),
                    IdAlumnoNavigationIdAlumno = table.Column<int>(type: "int", nullable: false),
                    IdComportamientoNavigationIdComportamiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoComportamiento", x => x.IdAlumnoComportamiento);
                    table.ForeignKey(
                        name: "FK_AlumnoComportamiento_Alumno_IdAlumnoNavigationIdAlumno",
                        column: x => x.IdAlumnoNavigationIdAlumno,
                        principalTable: "Alumno",
                        principalColumn: "IdAlumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlumnoComportamiento_Comportamiento_IdComportamientoNavigationIdComportamiento",
                        column: x => x.IdComportamientoNavigationIdComportamiento,
                        principalTable: "Comportamiento",
                        principalColumn: "IdComportamiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PadreReunion",
                columns: table => new
                {
                    IdPadreReunion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPadre = table.Column<int>(type: "int", nullable: true),
                    FirmaLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdReunion = table.Column<int>(type: "int", nullable: true),
                    IdCompromiso = table.Column<int>(type: "int", nullable: true),
                    IdCompromisoNavigationIdCompromiso = table.Column<int>(type: "int", nullable: true),
                    IdPadreNavigationIdPadre = table.Column<int>(type: "int", nullable: true),
                    IdReunionNavigationIdReunion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PadreReunion", x => x.IdPadreReunion);
                    table.ForeignKey(
                        name: "FK_PadreReunion_Compromiso_IdCompromisoNavigationIdCompromiso",
                        column: x => x.IdCompromisoNavigationIdCompromiso,
                        principalTable: "Compromiso",
                        principalColumn: "IdCompromiso");
                    table.ForeignKey(
                        name: "FK_PadreReunion_Padre_IdPadreNavigationIdPadre",
                        column: x => x.IdPadreNavigationIdPadre,
                        principalTable: "Padre",
                        principalColumn: "IdPadre");
                    table.ForeignKey(
                        name: "FK_PadreReunion_Reunion_IdReunionNavigationIdReunion",
                        column: x => x.IdReunionNavigationIdReunion,
                        principalTable: "Reunion",
                        principalColumn: "IdReunion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoComportamiento_IdAlumnoNavigationIdAlumno",
                table: "AlumnoComportamiento",
                column: "IdAlumnoNavigationIdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoComportamiento_IdComportamientoNavigationIdComportamiento",
                table: "AlumnoComportamiento",
                column: "IdComportamientoNavigationIdComportamiento");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_IdAlumnoNavigationIdAlumno",
                table: "Asistencia",
                column: "IdAlumnoNavigationIdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_Comportamiento_IdInformeNavigationIdInforme",
                table: "Comportamiento",
                column: "IdInformeNavigationIdInforme");

            migrationBuilder.CreateIndex(
                name: "IX_Compromiso_IdInformeNavigationIdInforme",
                table: "Compromiso",
                column: "IdInformeNavigationIdInforme");

            migrationBuilder.CreateIndex(
                name: "IX_Informe_IdPersonalNavigationIdPersonal",
                table: "Informe",
                column: "IdPersonalNavigationIdPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_IdAlumnoNavigationIdAlumno",
                table: "Nota",
                column: "IdAlumnoNavigationIdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_Padre_IdAlumnoNavigationIdAlumno",
                table: "Padre",
                column: "IdAlumnoNavigationIdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_PadreReunion_IdCompromisoNavigationIdCompromiso",
                table: "PadreReunion",
                column: "IdCompromisoNavigationIdCompromiso");

            migrationBuilder.CreateIndex(
                name: "IX_PadreReunion_IdPadreNavigationIdPadre",
                table: "PadreReunion",
                column: "IdPadreNavigationIdPadre");

            migrationBuilder.CreateIndex(
                name: "IX_PadreReunion_IdReunionNavigationIdReunion",
                table: "PadreReunion",
                column: "IdReunionNavigationIdReunion");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalEducativo_IdUsuarioNavigationId",
                table: "PersonalEducativo",
                column: "IdUsuarioNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reunion_IdInformeNavigationIdInforme",
                table: "Reunion",
                column: "IdInformeNavigationIdInforme");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoComportamiento");

            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropTable(
                name: "Nota");

            migrationBuilder.DropTable(
                name: "PadreReunion");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Comportamiento");

            migrationBuilder.DropTable(
                name: "Compromiso");

            migrationBuilder.DropTable(
                name: "Padre");

            migrationBuilder.DropTable(
                name: "Reunion");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "Informe");

            migrationBuilder.DropTable(
                name: "PersonalEducativo");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
