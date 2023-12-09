using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Colegio1258.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Data;
using Models.Views;

namespace Colegio1258.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Colegio1258Context _context;

        public StudentsController(Colegio1258Context context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult GetChart(string id)
        {
            int IdAlumno = Convert.ToInt32(id);
            if (IdAlumno != null) {
                var notas = _context.Nota.Where(x => x.IdAlumno == IdAlumno).ToList();
                //Porcentaje
                var iData = IndexGrades.CalculoProgreso(notas);

                return Json(iData);
            }
            else
            {
            return Json(null);
            }
        }


        #region "Views"
        public async Task<IActionResult> Index()
        {
            var _grado = ClaimsPrincipalExtentions.Grado();
            var _seccion = ClaimsPrincipalExtentions.Seccion();
            if (_grado != 0 && _seccion != null)
            {
                return RedirectToAction("List", "Students", new { Grado = _grado, Seccion = _seccion });
            }
            else
            {
                return RedirectToAction("YearStudents", "Students");
            }
        }
        public async Task<IActionResult> List(int Grado, string Seccion)
        {
            ViewData["Grado"] = Grado;
            ViewData["Seccion"] = Seccion.Trim();
            return _context.Alumnos != null ?
                        View(await _context.Alumnos.Where(x => x.Seccion.Trim().Equals(Seccion) && x.Grado == Grado).ToListAsync()) :
                        Problem("No existen registros");
        }

        [Authorize(Roles = "Administrador, Directivo")]
        public IActionResult YearStudents()
        {
            var model = _context.PersonalEducativos.Where(x=>x.Rol==1 || x.Rol==2).ToList();
            return View("../Students/YearStudents", model);
        }

        #endregion

        public IActionResult ImportData()
        {
            return View();
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = _context.Alumnos.Include(m=>m.Padres).Include(m=>m.AlumnoComportamientos).FirstOrDefault(m => m.IdAlumno == id);
            AlumnoModel model = new AlumnoModel().Detalles(alumno);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlumno,Grado,Seccion,TipoDocumento,NumeroDocumento,ValidadoReniec,CodigoEstudiante,ApellidoPaterno,ApellidoMaterno,Nombres,Sexo,FechaNacimiento,EstadoMatricula,SituacionMatricula,Pais,PadreVive,MadreVive,LenguaMaterna,SegundaLengua,TrabajaEstudiante,HorasLabora,EscolaridadMadre,NacimientoRegistrado,TipoDiscapacidad,CodigoModular,NombreRjrd,IdEstado,FechaCreacion,FechaModificacion,UserCreacion,UserModificacion")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alumnos == null)
            {
                return Problem("No existe entidad");
            }
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return (_context.Alumnos?.Any(e => e.IdAlumno == id)).GetValueOrDefault();
        }
    }
}
