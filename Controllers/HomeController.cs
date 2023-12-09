using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models.Views;
using Microsoft.AspNetCore.Authorization;
using Colegio1258.Repositories.Implementation;

namespace Colegio1258.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Colegio1258Context _context;

        public HomeController(Colegio1258Context context)
        {
            _context = context;
        }

        #region Métodos
        [HttpPost]
        public JsonResult ChartClasificacion()
        {
            List<object> iData = new List<object>();
            var mes = DateTime.Now.Month;
            List<Informe> reports = _context.Informes.Include(x => x.IdPersonalNavigation).Where(x => x.FechaCreacion.Value.Month == mes).ToList();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Mes", System.Type.GetType("System.String"));
            dt.Columns.Add("Informes", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Actas", System.Type.GetType("System.Int32"));


            Concepto con = new Concepto();
            for (int i = 2; i <= mes; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = con.MesNombre(i);
                dr[1] = (reports.Count != 0 ? reports.Where(x => x.ClasificacionAi == "I").Count() : 0);
                dr[2] = (reports.Count != 0 ? reports.Where(x => x.ClasificacionAi == "A").Count() : 0);
                dt.Rows.Add(dr);
            }

            //Looping and extracting each DataColumn to List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            return Json(iData);
        }


        [HttpPost]
        public JsonResult ChartTipo()
        {
            List<Informe> reports = _context.Informes.Include(x => x.IdPersonalNavigation).Where(x => x.FechaCreacion.Value.Month == DateTime.Now.Month).ToList();

            Object iData = new
            {
                //Attendance
                type1 = (reports.Count != 0 ? reports.Where(x => x.Tipo == 1).Count() : 1),
                //Behaviour
                type2 = (reports.Count != 0 ? reports.Where(x => x.Tipo == 2).Count() : 1),
                //Commitment
                type3 = (reports.Count != 0 ? reports.Where(x => x.Tipo == 3).Count() : 1),
                //Grades
                type4 = (reports.Count != 0 ? reports.Where(x => x.Tipo == 4).Count() : 1),
                //Extraordinary
                type5 = (reports.Count != 0 ? reports.Where(x => x.Tipo == 5).Count() : 1),
                //Ordinary
                type6 = (reports.Count != 0 ? reports.Where(x => x.Tipo == 6).Count() : 1)
            };
            return Json(iData);
        }

             
        #endregion

        #region "Views"
        public IActionResult Index()
        {
            var Rol = ClaimsPrincipalExtentions.RolPersonal();

            if (Rol == 1 || Rol == 2) //Docente
            {
                return RedirectToAction("IndexTeacher", "Home");
            }
            else if (Rol == 3) //Director
            {
                return RedirectToAction("IndexPrincipal", "Home");
            }
            else
            {
                return RedirectToAction("IndexAdmin", "Home");
            }

        }

        #region "Dashboard"
        [Authorize(Roles = "Administrador")]
        public IActionResult IndexAdmin()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        [Authorize(Roles = "Directivo")]
        public IActionResult IndexPrincipal()
        {
            int IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
            if (IdPersonal != 0)
            {
                PersonalEducativo per = _context.PersonalEducativos.Where(x => x.IdPersonal == IdPersonal).FirstOrDefault();
                List<Informe> reports = _context.Informes.Include(x => x.IdPersonalNavigation).ToList();
                List<Comportamiento> behaviors = _context.Comportamientos.Where(x => x.IdInformeNavigation.IdPersonal == IdPersonal).ToList();
                List<Reunion> sessions = _context.Reunions.Where(x => x.IdInformeNavigation.IdPersonal == IdPersonal).ToList();

                DirectorIndexModel model = new DirectorIndexModel();
                model.Detalles(per, reports, behaviors, sessions);
                return View("../Home/IndexPrincipal", model);
            }
            else
            {
                return RedirectToAction("ErrorData", "Home");
            }
        }

        [Authorize(Roles = "Docente")]
        public IActionResult IndexTeacher()
        {
            int IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
            if (IdPersonal != 0)
            {
                PersonalEducativo per = _context.PersonalEducativos.Where(x => x.IdPersonal == IdPersonal).FirstOrDefault();
                List<Informe> reports = _context.Informes.Include(x => x.IdPersonalNavigation).Where(x => x.IdPersonal == IdPersonal && x.EstadoInforme=="P" || x.EstadoInforme=="R").ToList();
                List<Comportamiento> behaviors = _context.Comportamientos.Where(x => x.IdInformeNavigation.IdPersonal == IdPersonal).ToList();
                List<Reunion> sessions = _context.Reunions.Where(x => x.IdInformeNavigation.IdPersonal == IdPersonal).ToList();

                DocenteIndexModel model = new DocenteIndexModel();
                model.Detalles(per, reports, behaviors, sessions);

                return View("../Home/IndexTeacher", model);
            }
            else
            {
                return RedirectToAction("ErrorData", "Home");
            }
          
        }
        #endregion


        #region "Profile"
        public IActionResult Profile()
        {
            int IdPersonal = ClaimsPrincipalExtentions.IdPersonal();
            PersonalEducativo model = _context.PersonalEducativos.Where(x => x.IdPersonal == IdPersonal).FirstOrDefault();
                       
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(PersonalEducativo personal)
        {
            //Buscar modelo
            PersonalEducativo pe = _context.PersonalEducativos.Where(x => x.IdPersonal == personal.IdPersonal).FirstOrDefault();
            pe.Dni = personal.Dni;
            pe.Telefono = personal.Telefono;
            pe.Direccion = personal.Direccion;
            pe.FechaNacimiento = personal.FechaNacimiento;
            pe.CorreoElectronico = personal.CorreoElectronico;

            if (ModelState.IsValid)
            {
                _context.Add(pe);
                await _context.SaveChangesAsync();
                TempData["msg"] = "Datos actualizados";
                TempData["status"] = 1;
                return RedirectToAction(nameof(Profile));
            }
            TempData["msg"] = "Verifique los datos ingresados";
            TempData["status"] = 0;
            return View();
        }
        #endregion

        public IActionResult Reports()
        {
            return View("../Reports/ReportTypes");
        }

        public IActionResult ReportList(int type)
        {
            return View("../ReportList", type);
        }

        public IActionResult CreateReport(int type)
        {
            switch (type)
            {
                case 1: return View("../Report/ReportCreateAttendance");
                case 2: return View("../Report/ReportCreateBehavior");
                case 3: return View("../Report/ReportCreateCommitment");
                case 4: return View("../Report/ReportCreateGrades");
                case 5: return View("../Report/ReportCreateExtraordinario");
                case 6: return View("../Report/ReportCreateOrdinario");
            }
            return View("../ReportTypes");
        }


       

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}


        public IActionResult Error()
        {
            return View("../Error/Error404");
        }

        public IActionResult ErrorData()
        {
            return View("../Error/ErrorData");
        }
        #endregion
    }
}