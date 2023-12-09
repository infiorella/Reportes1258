using Informes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Reports;
using Models.Views;
using Informes;
using QuestPDF.Fluent;
using System.Net.Mail;
using System.Net;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using Colegio1258.Repositories.Implementation;
using SkiaSharp;
using Microsoft.Win32;
using System.Net.Mime;

namespace Colegio1258.Controllers
{
    public class ReportsController : Controller
    {
        private readonly Colegio1258Context _context;

        public ReportsController(Colegio1258Context context)
        {
            _context = context;
        }

        #region "Views"
        public IActionResult Index()
        {
            int rol = ClaimsPrincipalExtentions.RolPersonal();
            if (rol == 1)
            {
                return View();
            }
            else if (rol == 3)
            {
                var list = _context.Informes.ToList();
                var model = ReportModel.DetailsPrincipal(list);

                return RedirectToAction(nameof(ListPrincipal));
            }
            else
            {
                return RedirectToAction(nameof(ListAdmin));
            }

        }

        public IActionResult ListPrincipal()
        {
            var list = _context.Informes.Include(x=>x.IdPersonalNavigation).Where(x => x.EstadoInforme.Trim() == "E" || x.EstadoInforme.Trim() == "A").ToList();
            var model = ReportModel.DetailsPrincipal(list);

            return View("../Reports/ListPrincipal", model);
        }

        public IActionResult ListAdmin()
        {
            var list = _context.Informes.ToList();
            var model = ReportModel.DetailsPrincipal(list);

            return View("../Reports/ListAdmin", model);
        }

        public IActionResult List(int type)
        {
            int id = ClaimsPrincipalExtentions.IdPersonal();

            var list = _context.Informes.Where(x => x.Tipo == type && x.IdPersonal == id).ToList();

            var model = ReportModel.Details(list);

            Concepto con = new Concepto();
            ViewData["Type"] = con.TipoInforme(type);
            ViewData["Class"] = con.ClasificacionInforme(type);

            return View(model);
        }

        public IActionResult Create(int type)
        {
            int IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
            int Grado = ClaimsPrincipalExtentions.Grado();
            string Seccion = ClaimsPrincipalExtentions.Seccion();
            Informe report = new Informe();
            report.IdPersonalNavigation = _context.PersonalEducativos.Where(x => x.IdPersonal == IdPersonal).FirstOrDefault();

            switch (type)
            {
                case 1:
                    return View("../Reports/ReportCreateAttendance", report);

                case 2:
                    var students = _context.Alumnos.
                        Where(x => x.Grado == report.IdPersonalNavigation.Grado && x.Seccion == report.IdPersonalNavigation.Seccion)
                        .Select(x => new
                        {
                            IdAlumno = x.IdAlumno,
                            Nombres = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno
                        }).
                        ToList();
                    ViewData["IdAlumnos"] = new SelectList(students, "IdAlumno", "Nombres");
                    Comportamiento behavior = new Comportamiento();
                    behavior.IdInformeNavigation = report;
                    return View("../Reports/ReportCreateBehavior", behavior);

                case 3:
                    var parents = _context.Padres.
                        Where(x => x.IdAlumnoNavigation.Seccion == report.IdPersonalNavigation.Seccion && x.IdAlumnoNavigation.Grado == report.IdPersonalNavigation.Grado)
                         .Select(x => new
                         {
                             IdPadre = x.IdPadre,
                             Nombres = x.Nombres + " " + x.Apellidos
                         }).
                        ToList();
                    ViewData["IdPadres"] = new SelectList(parents, "IdPadre", "Nombres");
                    Compromiso commitment = new Compromiso();
                    commitment.IdInformeNavigation = report;
                    return View("../Reports/ReportCreateCommitment", commitment);

                case 4:
                    return View("../Reports/ReportCreateGrades", report);

                case 5:
                    var parentsExtra = _context.Padres.
                        Where(x => x.IdAlumnoNavigation.Seccion == report.IdPersonalNavigation.Seccion && x.IdAlumnoNavigation.Grado == report.IdPersonalNavigation.Grado)
                         .Select(x => new
                         {
                             IdPadre = x.IdPadre,
                             Nombres = x.Nombres + " " + x.Apellidos
                         }).
                        ToList();
                    ViewData["IdPadres"] = new SelectList(parentsExtra, "IdPadre", "Nombres");
                    Reunion sessionExtra = new Reunion();
                    sessionExtra.IdInformeNavigation = report;
                    return View("../Reports/ReportCreateExtraordinary", sessionExtra);

                case 6:
                    var padresOrdi = _context.Padres.
                        Where(x => x.IdAlumnoNavigation.Seccion == report.IdPersonalNavigation.Seccion && x.IdAlumnoNavigation.Grado == report.IdPersonalNavigation.Grado)
                         .Select(x => new
                         {
                             IdPadre = x.IdPadre,
                             Nombres = x.Nombres + " " + x.Apellidos
                         }).
                        ToList();
                    ViewData["IdPadres"] = new SelectList(padresOrdi, "IdPadre", "Nombres");
                    Reunion sessionOrdi = new Reunion();
                    sessionOrdi.IdInformeNavigation = report;
                    return View("../Reports/ReportCreateOrdinary", sessionOrdi);
            }
            return View("../ReportTypes");
        }

        public IActionResult ReportPDF(int IdInforme)
        {
            var Link = _context.Informes.Where(c => c.IdInforme == IdInforme)
                    .Select(c => c.Link)
                    .FirstOrDefault();

            TempData["link"] = Link;

            return View();
        }
        #endregion


        #region " Creates "      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAttendance(Informe report)
        {
            try
            {

                Informe saveReport = new Informe();
                _context.Attach(saveReport);

                saveReport.IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
                saveReport.ClasificacionAi = "I";
                saveReport.Tipo = 1;
                saveReport.Titulo = "Informe de asistencia del " + report.IdPersonalNavigation.Grado + " '" + report.IdPersonalNavigation.Seccion + "' para el mes de " + report.Mes;
                saveReport.Descripcion = "";
                saveReport.Mes = report.Mes;
                saveReport.Link = "";
                saveReport.Comentario = report.Comentario;
                saveReport.EstadoInforme = "P";
                saveReport.IdEstado = 1;
                saveReport.FechaCreacion = DateTime.Now;

                _context.SaveChanges();

                report.IdInforme = saveReport.IdInforme;
                return RedirectToAction("PreviewAttendance", report);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["status"] = 0;
                return View(report);
            }
        }

        public IActionResult PreviewAttendance(Informe report)
        {
            return View(report);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBehavior(Comportamiento comp)
        {
            try
            {
                Informe saveReport = new Informe();
                _context.Attach(saveReport);

                saveReport.IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
                saveReport.ClasificacionAi = "I";
                saveReport.Tipo = 2;
                saveReport.Titulo = "Informe de comportamiento del " + comp.IdInformeNavigation.IdPersonalNavigation.Grado + " '" + comp.IdInformeNavigation.IdPersonalNavigation.Seccion + "' para el mes de " + comp.IdInformeNavigation.Mes;
                saveReport.Descripcion = "";
                saveReport.Mes = comp.IdInformeNavigation.Mes;
                saveReport.Link = "";
                saveReport.Comentario = comp.IdInformeNavigation.Comentario;
                saveReport.EstadoInforme = "P";
                saveReport.IdEstado = 1;
                saveReport.FechaCreacion = DateTime.Now;

                _context.Informes.Add(saveReport);
                _context.SaveChanges();

                _context.Comportamientos.Add(comp);
                _context.SaveChanges();

                return RedirectToAction("PreviewBehavior", comp);


            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["status"] = 0;
                return View(comp);
            }
        }

        public IActionResult PreviewBehavior(Comportamiento comp)
        {
            return View(comp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCommitment(Compromiso comp)
        {
            try
            {
                Informe saveReport = new Informe();
                _context.Attach(saveReport);

                saveReport.IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
                saveReport.ClasificacionAi = "A";
                saveReport.Tipo = 3;
                saveReport.Titulo = "Informe de compromiso del " + comp.IdInformeNavigation.IdPersonalNavigation.Grado + " '" + comp.IdInformeNavigation.IdPersonalNavigation.Seccion + "'";
                saveReport.Descripcion = "";
                saveReport.Mes = comp.IdInformeNavigation.Mes;
                saveReport.Link = "";
                saveReport.Comentario = comp.IdInformeNavigation.Comentario;
                saveReport.EstadoInforme = "P";
                saveReport.IdEstado = 1;
                saveReport.FechaCreacion = DateTime.Now;

                //Save report
                _context.Informes.Add(saveReport);
                _context.SaveChanges();

                //save commitemnt
                _context.Compromisos.Add(comp);
                _context.SaveChanges();

                return View("PreviewCommitment", comp);

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["status"] = 0;
                return View(comp);
            }
        }

        public IActionResult PreviewCommitment(Comportamiento comp)
        {
            return View(comp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExtraordinary(Reunion reu, List<int> padres)
        {
            try
            {
                if (reu != null)
                {
                    Informe saveReport = new Informe();

                    _context.Attach(saveReport);

                    saveReport.IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
                    saveReport.ClasificacionAi = "A";
                    saveReport.Tipo = 5;
                    saveReport.Titulo = "Acta de Sesión extraordinaria del día " + reu.FechaReunion + "  " + reu.IdInformeNavigation.IdPersonalNavigation.Grado + " '" + reu.IdInformeNavigation.IdPersonalNavigation.Seccion + "'";
                    saveReport.Descripcion = "";
                    saveReport.Mes = reu.IdInformeNavigation.Mes;
                    saveReport.Link = "";
                    saveReport.Comentario = reu.IdInformeNavigation.Comentario;
                    saveReport.EstadoInforme = "P";
                    saveReport.IdEstado = 1;
                    saveReport.FechaCreacion = DateTime.Now;

                    //Save report
                    _context.SaveChanges();

                    //Save reunion
                    reu.IdInforme = (saveReport.IdInforme);

                    //Save Parents repots
                    foreach (var item in padres)
                    {
                        //int reunionid = _context.Reunions.Where(x => x.IdInforme == saveReport.IdInforme).Select(x=>x.IdReunion).FirstOrDefault();
                        PadreReunion padre = new PadreReunion();
                        padre.IdPadre = item;
                        padre.IdReunion = 1;

                        _context.PadreReunions.Add(padre);
                        _context.SaveChanges();
                    }


                    return RedirectToAction("PreviewExtraordinary", reu);
                }
                TempData["msg"] = "Verifique los datos ingresados";
                TempData["status"] = 0;

                return View(reu);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["status"] = 0;
                return View(reu);
            }
        }

        public IActionResult PreviewExtraordinary(Reunion reu)
        {
            return View(reu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGrades(Informe report)
        {
            try
            {
                Informe saveReport = new Informe();
                _context.Attach(saveReport);

                saveReport.IdPersonal = ClaimsPrincipalExtentions.IdPersonal(); ;
                saveReport.ClasificacionAi = "I";
                saveReport.Tipo = 4;
                saveReport.Titulo = "Informe de notas del " + report.IdPersonalNavigation.Grado + report.IdPersonalNavigation.Seccion + " para el mes de " + report.Mes;
                saveReport.Descripcion = "";
                saveReport.Mes = report.Mes;
                saveReport.Link = "";
                saveReport.Comentario = report.Comentario;
                saveReport.EstadoInforme = "P";
                saveReport.IdEstado = 1;
                saveReport.FechaCreacion = DateTime.Now;

                _context.SaveChanges();
                report.IdInforme = saveReport.IdInforme;
                return RedirectToAction("PreviewGrades", report);

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["status"] = 0;
                return View(report);
            }
        }

        public IActionResult PreviewGrades(Informe report)
        {
            return View(report);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrdinary(Reunion reu)
        {
            try
            {
                Informe infor = new Informe();

                infor.IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
                infor.ClasificacionAi = "A";
                infor.Tipo = 6;
                infor.Titulo = "Informe de compromiso del " + reu.IdInformeNavigation.IdPersonalNavigation.Grado + " '" + reu.IdInformeNavigation.IdPersonalNavigation.Seccion + "'";
                infor.Descripcion = "";
                infor.Mes = reu.IdInformeNavigation.Mes;
                infor.Link = "";
                infor.Comentario = reu.IdInformeNavigation.Comentario;
                infor.EstadoInforme = "P";
                infor.IdEstado = 1;
                infor.FechaCreacion = DateTime.Now;

                _context.Informes.Add(infor);
                _context.SaveChanges();

                //You can get the ID here
                int IdInform = infor.IdInforme;

                if (IdInform != null || IdInform != 0)
                {
                    reu.IdInforme = IdInform;

                    _context.Reunions.Add(reu);
                    _context.SaveChanges();
                    return RedirectToAction("ViewPDFExtraordinary", reu);
                }
                else
                {
                    TempData["msg"] = "No se puede generar el informe";
                    TempData["status"] = 0;

                    return View(reu);
                }


            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["status"] = 0;
                return View(reu);
            }
        }

        public IActionResult PreviewGrades(Reunion reu)
        {
            return View(reu);
        }

        #endregion

        #region "Views PDF"
        public ActionResult ViewPDFAttendance(Informe inf)
        {
            inf.IdPersonalNavigation = _context.PersonalEducativos.Where(x => x.IdPersonal == 1).FirstOrDefault();

            //DATOS
            List<Asistencia> asistencia = _context.Asistencia.Where(x => x.FechaAsistencia.Value.Month == inf.Mes).ToList();

            //Obtener los datos
            LNInformeAsistencia modelo = new LNInformeAsistencia(inf, asistencia);

            //Mostrar informe
            var document = new TemplateAttendance(modelo);
            var data = document.GeneratePdf();

            Stream stream = new MemoryStream(data);

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ViewPDFBehavior(Comportamiento comp)
        {
            comp.IdInformeNavigation = _context.Informes.Include(x => x.IdPersonalNavigation).Where(x => x.IdInforme == comp.IdInforme).FirstOrDefault();

            LNInformeComportamiento model = new LNInformeComportamiento(comp);

            var document = new TemplateBehavior(model);
            var data = document.GeneratePdf();

            Stream stream = new MemoryStream(data);

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ViewPDFCommitment(Compromiso comp)
        {
            comp.IdInformeNavigation = _context.Informes.Include(x => x.IdPersonalNavigation).Where(x => x.IdInforme == comp.IdInforme).FirstOrDefault();

            LNActaCompromiso model = new LNActaCompromiso(comp);

            var document = new TemplateCommitment(model);
            var data = document.GeneratePdf();

            Stream stream = new MemoryStream(data);

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ViewPDFGrades(Informe inf)
        {
            //DATOS
            List<Nota> grades = _context.Nota.Where(x => x.Grado == inf.IdPersonalNavigation.Grado && x.Seccion == inf.IdPersonalNavigation.Seccion).ToList();

            LNInformeNotas model = new LNInformeNotas(inf, grades);

            var document = new TemplateGrades(model);
            var data = document.GeneratePdf();

            Stream stream = new MemoryStream(data);

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ViewPDFExtraordinary(Reunion reu)
        {
            reu.IdInformeNavigation = _context.Informes.Include(x => x.IdPersonalNavigation).Where(x => x.IdInforme == reu.IdInforme).FirstOrDefault();
            var nombres = _context.PadreReunions.Include(x => x.IdPadreNavigation).Where(x => x.IdReunion == reu.IdReunion).Select(c => new { c.IdPadreNavigation.Nombres, c.IdPadreNavigation.Apellidos }).ToList();
            List<string> lista = new List<string>();
            foreach (var item in nombres)
            {
                string datos = item.Nombres + " " + item.Apellidos;
                lista.Add(datos);
            }

            //DATOS
            LNActaSesionExtraordinaria model = new LNActaSesionExtraordinaria(reu, lista);
            var document = new TemplateExtraordinaria(model);
            var data = document.GeneratePdf();

            Stream stream = new MemoryStream(data);

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ViewPDFOrdinary(Reunion reu)
        {
            reu.IdInformeNavigation = _context.Informes.Include(x => x.IdPersonalNavigation).Where(x => x.IdInforme == reu.IdInforme).FirstOrDefault();

            //DATOS
            List<Nota> notasreu = _context.Nota.Where(x => x.IdAlumnoNavigation.Grado == reu.IdInformeNavigation.IdPersonalNavigation.Grado && x.IdAlumnoNavigation.Seccion == reu.IdInformeNavigation.IdPersonalNavigation.Seccion).ToList();

            LNActaSesionOrdinaria ordinaria = new LNActaSesionOrdinaria(reu, notasreu);
            var document = new TemplateOrdinaria(ordinaria);

            var data = document.GeneratePdf();

            Stream stream = new MemoryStream(data);

            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        #endregion
        public IActionResult Edit(int id)
        {
            var informe = _context.Informes.Where(x => x.IdInforme == id).Include(x => x.IdPersonalNavigation).FirstOrDefault();

            switch (informe.Tipo)
            {
                case 1:
                    return View("../Reports/ReportCreateAttendance", informe);

                case 2:
                    var students = _context.Alumnos.
                    Where(x => x.Grado == informe.IdPersonalNavigation.Grado && x.Seccion == informe.IdPersonalNavigation.Seccion)
                    .Select(x => new
                    {
                        IdAlumno = x.IdAlumno,
                        Nombres = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno
                    }).
                        ToList();
                    ViewData["IdAlumnos"] = new SelectList(students, "IdAlumno", "Nombres");
                    Comportamiento behavior = new Comportamiento();
                    behavior.IdInformeNavigation = informe;
                    return View("../Reports/ReportCreateBehavior", behavior);

                case 3:
                    var parents = _context.Padres.
                    Where(x => x.IdAlumnoNavigation.Seccion == informe.IdPersonalNavigation.Seccion && x.IdAlumnoNavigation.Grado == informe.IdPersonalNavigation.Grado)
                    .Select(x => new
                    {
                        IdPadre = x.IdPadre,
                        Nombres = x.Nombres + " " + x.Apellidos
                    }).
                        ToList();
                    ViewData["IdPadres"] = new SelectList(parents, "IdPadre", "Nombres");
                    Compromiso commitment = new Compromiso();
                    commitment.IdInformeNavigation = informe;
                    return View("../Reports/ReportCreateCommitment", commitment);

                case 4:
                    return View("../Reports/ReportCreateGrades", informe);

                case 5:
                    var parentsExtra = _context.Padres.
                    Where(x => x.IdAlumnoNavigation.Seccion == informe.IdPersonalNavigation.Seccion && x.IdAlumnoNavigation.Grado == informe.IdPersonalNavigation.Grado)
                    .Select(x => new
                    {
                        IdPadre = x.IdPadre,
                        Nombres = x.Nombres + " " + x.Apellidos
                    }).
                        ToList();
                    ViewData["IdPadres"] = new SelectList(parentsExtra, "IdPadre", "Nombres");
                    Reunion sessionExtra = new Reunion();
                    sessionExtra.IdInformeNavigation = informe;
                    return View("../Reports/ReportCreateExtraordinary", sessionExtra);

                case 6:
                    var padresOrdi = _context.Padres.
                    Where(x => x.IdAlumnoNavigation.Seccion == informe.IdPersonalNavigation.Seccion && x.IdAlumnoNavigation.Grado == informe.IdPersonalNavigation.Grado)
                    .Select(x => new
                    {
                        IdPadre = x.IdPadre,
                        Nombres = x.Nombres + " " + x.Apellidos
                    }).
                        ToList();
                    ViewData["IdPadres"] = new SelectList(padresOrdi, "IdPadre", "Nombres");
                    Reunion sessionOrdi = new Reunion();
                    sessionOrdi.IdInformeNavigation = informe;
                    return View("../Reports/ReportCreateOrdinary", sessionOrdi);
            }
            return View("../ReportTypes");
        }


        public IActionResult SendEmail(int id)
        {
            try
            {
                string file = "./wwwroot/Documents/ViewPDFAttendance.pdf";
                string GmailAccount = "vasquezani2@gmail.com";
                string GmailAppPassword = "ussefahrguqbwdhd";

                string To = "vasquezani2@gmail.com";

                MailMessage mail = new(GmailAccount, To)
                {
                    Subject = "Informe de Asistencia",
                    IsBodyHtml = true,
                    Body = "<p >Buen día, le adjunto el documento en PDF del informe de asistencia </p><br>Prueba Comentario"
                };

                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                // Add the file attachment to this email message.
                mail.Attachments.Add(data);

                SmtpClient smtp = new("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(GmailAccount, GmailAppPassword),
                };

                System.Diagnostics.Debug.WriteLine("---enviando---");
                smtp.Send(mail);

                var informe = _context.Informes.Where(x => x.IdInforme == id).FirstOrDefault();
                ChangeStatus(informe);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("---error.--");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(List), new { type = 1 });
        }

        #region "Aprobar/Desaprobar"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int IdInforme, string comentario)
        {
            var informe = _context.Informes.Where(x => x.IdInforme == IdInforme).FirstOrDefault();
            try
            {
                if (informe != null)
                {
                    informe.EstadoInforme = "A";
                    informe.ComentarioDirector = comentario;
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Informe Aprobado" });
                }
                else
                {
                    return Json(new { success = false, message = "No se encontró el informe" });
                }
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public void ChangeStatus(Informe informe)
        {
            try
            {
                if (informe != null)
                {
                    informe.EstadoInforme = "E";
                   _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                string ea = ex.Message;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disapprove(int IdInforme, string comentario)
        {
            var informe = _context.Informes.Where(x => x.IdInforme == IdInforme).FirstOrDefault();
            try
            {
                if (informe != null)
                {
                    informe.EstadoInforme = "A";
                    informe.ComentarioDirector = comentario;
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Informe Aprobado" });
                }
                else
                {
                    return Json(new { success = false, message = "No se encontró el informe" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
        #endregion
    }
}


//public ActionResult ViewPDFAttendance(int IdReporte)
//{
//    Informe infor = _context.Informes.Where(x => x.IdInforme == IdReporte).Include(x => x.IdPersonalNavigation).FirstOrDefault();
//    List<Asistencia> asistencia = _context.Asistencia.Where(x => x.IdAlumnoNavigation.Grado == infor.IdPersonalNavigation.Grado && x.IdAlumnoNavigation.Seccion == infor.IdPersonalNavigation.Seccion).ToList();
//    LNInformeAsistencia informeAsis = new LNInformeAsistencia(infor, asistencia);
//    var InformeAsistencia = new TemplateAttendance(informeAsis);
//    var data = InformeAsistencia.GeneratePdf();
//    Stream stream = new MemoryStream(data);

//    stream.Write(data, 0, data.Length);
//    stream.Position = 0;
//    return new FileStreamResult(stream, "application/pdf");
//}




//public ActionResult ViewPDFBehaviour(int IdReporte)
//{
//    Informe infor = _context.Informes.Where(x => x.IdInforme == IdReporte).Include(x => x.IdPersonalNavigation).FirstOrDefault();
//    Comportamiento comportamiento = _context.Comportamientos.Where(x => x.IdInforme == infor.IdInforme).Include(x => x.IdInformeNavigation).FirstOrDefault();
//    LNInformeComportamiento informeCom = new LNInformeComportamiento(comportamiento);
//    var InformeComportamiento = new TemplateBehavior(informeCom);
//    var data = InformeComportamiento.GeneratePdf();

//    Stream stream = new MemoryStream(data);
//    stream.Write(data, 0, data.Length);
//    stream.Position = 0;
//    return new FileStreamResult(stream, "application/pdf");
//}




//public ActionResult ViewPDFCommitment(int IdReporte)
//{
//    Informe infor = _context.Informes.Where(x => x.IdInforme == IdReporte).Include(x => x.IdPersonalNavigation).FirstOrDefault();
//    Compromiso comp = _context.Compromisos.Where(x => x.IdInforme == IdReporte).Include(x => x.IdInformeNavigation).FirstOrDefault();
//    LNActaCompromiso informeCompro = new LNActaCompromiso(comp);
//    var InformeCompromiso = new TemplateCommitment(informeCompro);
//    var data = InformeCompromiso.GeneratePdf();

//    Stream stream = new MemoryStream(data);
//    stream.Write(data, 0, data.Length);
//    stream.Position = 0;
//    return new FileStreamResult(stream, "application/pdf");
//}




//public ActionResult ViewPDFExtraordinario(int IdReporte)
//{
//    Informe infor = _context.Informes.Where(x => x.IdInforme == IdReporte).Include(x => x.IdPersonalNavigation).FirstOrDefault();

//    Reunion reuex = _context.Reunions.Where(x => x.IdInforme == IdReporte).Include(x => x.IdInformeNavigation).FirstOrDefault();
//    LNActaSesionExtraordinaria informeExtra = new LNActaSesionExtraordinaria(reuex);
//    var InformeExtraordinaria = new TemplateExtraordinaria(informeExtra);
//    var data = InformeExtraordinaria.GeneratePdf();

//    Stream stream = new MemoryStream(data);
//    stream.Write(data, 0, data.Length);
//    stream.Position = 0;
//    return new FileStreamResult(stream, "application/pdf");
//}




//public ActionResult ViewPDFGrades(int IdReporte)
//{
//    Informe infor = _context.Informes.Where(x => x.IdInforme == IdReporte).Include(x => x.IdPersonalNavigation).FirstOrDefault();

//    List<Nota> notas = _context.Nota.Where(x => x.IdAlumnoNavigation.Grado == infor.IdPersonalNavigation.Grado && x.IdAlumnoNavigation.Seccion == infor.IdPersonalNavigation.Seccion).ToList();
//    LNInformeNotas informeNotas = new LNInformeNotas(infor, notas);
//    var InformeNotas = new TemplateGrades(informeNotas);
//    var data = InformeNotas.GeneratePdf();

//    Stream stream = new MemoryStream(data);
//    stream.Write(data, 0, data.Length);
//    stream.Position = 0;
//    return new FileStreamResult(stream, "application/pdf");
//}


//public ActionResult ViewPDFOrdinario(int IdReporte)
//{
//    Informe infor = _context.Informes.Where(x => x.IdInforme == IdReporte).Include(x => x.IdPersonalNavigation).FirstOrDefault();

//    Reunion reuor = _context.Reunions.Where(x => x.IdInforme == IdReporte).FirstOrDefault();
//    List<Nota> notasreu = _context.Nota.Where(x => x.IdAlumnoNavigation.Grado == infor.IdPersonalNavigation.Grado && x.IdAlumnoNavigation.Seccion == infor.IdPersonalNavigation.Seccion).ToList();
//    LNActaSesionOrdinaria informeOrdi = new LNActaSesionOrdinaria(reuor, notasreu);
//    var InformeOrdinaria = new TemplateOrdinaria(informeOrdi);
//    var data = InformeOrdinaria.GeneratePdf();

//    Stream stream = new MemoryStream(data);
//    stream.Write(data, 0, data.Length);
//    stream.Position = 0;
//    return new FileStreamResult(stream, "application/pdf");
//}
