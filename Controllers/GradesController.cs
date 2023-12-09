using Colegio1258.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Data;
using Models.Views;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Security.Claims;

namespace Colegio1258.Controllers
{
    public class GradesController : Controller
    {
        private readonly Colegio1258Context _context;

        public GradesController(Colegio1258Context context)
        {
            _context = context;
        }


        #region methods

        public JsonResult GetSkills(int Grado, string Seccion, string Area)
        {
            var lista = _context.Nota
                .Where(x => x.AreaCurricular
           .Equals(Area.Trim()) && x.Seccion.Trim() == Seccion.Trim() && x.Grado == Grado)
                .GroupBy(x => new { x.Grado, x.Seccion, x.AreaCurricular, x.Competencia })
                .Select(y => new
                {
                    Competencia=y.Key.Competencia
                })
                .ToList();

         
            return Json(lista);
        }


        public List<IndexGrades>? GetTableGrades(int Grado, string Seccion, string Area, string Skill)
        {
            List<Nota?> NotasLista= new List<Nota?>();
            if (Skill != null)
            {
                NotasLista = _context.Nota.Include(n => n.IdAlumnoNavigation).Where(x => x.AreaCurricular
                .Equals(Area) && x.Seccion.Trim() == Seccion && x.Grado == Grado && x.Competencia.Trim() == Skill).ToList();
            }
            else
            {
                NotasLista = _context.Nota.Include(n => n.IdAlumnoNavigation).Where(x => x.AreaCurricular
                .Equals(Area) && x.Seccion.Trim() == Seccion && x.Grado == Grado).ToList();
            }           

            List<IndexGrades> lista = IndexGrades.ObtenerTabla(NotasLista);

            return lista;            
        }



        public JsonResult DetailsGrades(int Grado, string Seccion, string Area, string Skill)
        {
            //Obtener datos
            var NotasLista = _context.Nota.Where(x => x.AreaCurricular
            .Equals(Area) && x.Seccion.Trim() == Seccion && x.Grado == Grado && x.Competencia.Trim()==(Skill)).ToList();

            if (NotasLista != null)
            {
                var datos = IndexGrades.CalculoProgreso(NotasLista);
                return Json(datos);
            }
            else
            {
                return Json(null);
            }
        }

        public JsonResult DetailsGradesGeneral(int Grado, string Seccion, string Area)
        {
            //Obtener datos
            var NotasLista = _context.Nota.Where(x => x.AreaCurricular
            .Equals(Area) && x.Seccion.Trim() == Seccion && x.Grado == Grado).ToList();

            if (NotasLista != null)
            {
                var datos = IndexGrades.CalculoProgreso(NotasLista);
                return Json(datos);
            }
            else
            {
                return Json(null);
            }
        }
        #endregion

        #region "Views"
        public IActionResult Courses(int Grado, string Seccion)
        {
            TempData["Grado"] = Grado;
            TempData["Seccion"] = Seccion;
            return View();
        }
        [Authorize(Roles = "Administrador, Directivo")]
        public IActionResult YearStudents()
        {
            var model = _context.PersonalEducativos.Where(x => x.Rol == 1).ToList();
            return View("../Grades/YearStudents", model);
        }

        public IActionResult ImportData()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var _grado = ClaimsPrincipalExtentions.Grado();
            var _seccion = ClaimsPrincipalExtentions.Seccion();
            if (_grado!=0 && _seccion!=null)
            {
                return RedirectToAction("Courses", "Grades", new { Grado = _grado, Seccion = _seccion});
            }
            else
            {
                return RedirectToAction("YearStudents", "Grades");
            }
        }
        
        public async Task<IActionResult> List(int Grado, string Seccion, string Area)
        {
            TempData["Grado"] = Grado;
            TempData["Seccion"] = Seccion;
            TempData["Area"] = Area; 
            return View();
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var Nota = await _context.Nota
                .Include(n => n.IdAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNota == id);
            if (Nota == null)
            {
                return NotFound();
            }

            return View(Nota);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno");
            return View();
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNota,IdAlumno,AreaCurricular,Competencia,Cal1,Cal2,Cal3,CalAnual,Comentario,IdEstado,FechaCreacion,UserCreacion")] Nota Nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", Nota.IdAlumno);
            return View(Nota);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportData(Nota Nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", Nota.IdAlumno);
            return View(Nota);
        }

    }
}
