using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Colegio1258.Repositories.Implementation;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Data;
using Models.Views;

namespace Colegio1258.Controllers
{
    public class AttendanceController : Controller
    {
        #region propiedades
        private readonly Colegio1258Context _context;

        #endregion

        public AttendanceController(Colegio1258Context context)
        {
            _context = context;
        }


        #region methods

        public List<List<string>>? ObtenerAsistenciaScript(int year, int month, int Grado, string Seccion)
        {           
           var Model = _context.Asistencia.Where(x => x.FechaAsistencia.Value.Month == month
           && x.FechaAsistencia.Value.Year == year && x.IdAlumnoNavigation.Grado == Grado
           && x.IdAlumnoNavigation.Seccion.Trim().Equals(Seccion.Trim())).Include(a => a.IdAlumnoNavigation).ToList();

            var table = IndexAttendance.ObtenerTabla(Model, year, month);

            return table;
        }

        public List<int>? ObtenerDias(int year, int month)
        {
            List<int> daysOfMonth = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day).Day)
                .ToList();

            return daysOfMonth;
        }      

        public JsonResult DetallesAsistencia(int Grado, string Seccion, int Month, int Year)
        {
            var AsistenciaLista = _context.Asistencia.Where(x => x.FechaAsistencia.Value.Month == Month
           && x.FechaAsistencia.Value.Year == Year && x.IdAlumnoNavigation.Grado == Grado
           && x.IdAlumnoNavigation.Seccion.Trim().Equals(Seccion.Trim())).Include(a => a.IdAlumnoNavigation).ToList();

            if (AsistenciaLista != null) {
                var datos = IndexAttendance.CalculoAsistencia(AsistenciaLista);
                return Json(datos);
            }
            else {
                return Json(null);
            }            
        }
        #endregion

        #region "Views"
        // GET: Attendance
        public async Task<IActionResult> Index()
        {
            var _grado = ClaimsPrincipalExtentions.Grado();
            var _seccion = ClaimsPrincipalExtentions.Seccion();
            if (_grado!=0 && _seccion!=null)
            {
                return RedirectToAction("List", "Attendance", new { Grado = _grado, Seccion = _seccion });
            }
            else
            {
                return RedirectToAction("YearStudents", "Attendance");
            }
        }

        public async Task<IActionResult> List(int Grado, string Seccion)
        {
            TempData["Grado"] = Grado;
            TempData["Seccion"] = Seccion;

            return View();
        }


        [Authorize(Roles = "Administrador, Directivo")]
        public async Task<IActionResult> YearStudents()
        {
            var model = _context.PersonalEducativos.Where(x=>x.Rol==1).ToList();
            return View("../Attendance/YearStudents", model);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult ImportData()
        {
            return View();
        }

        // GET: Attendance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var Asistencia = await _context.Asistencia
                .Include(a => a.IdAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.IdAsistencia == id);
            if (Asistencia == null)
            {
                return NotFound();
            }

            return View(Asistencia);
        }

        // GET: Attendance/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAsistencia,FechaAsistencia,IdAlumno,Estado,FechaCreacion,UserCreacion")] Asistencia Asistencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", Asistencia.IdAlumno);
            return View(Asistencia);
        }

        // GET: Attendance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var Asistencia = await _context.Asistencia.FindAsync(id);
            if (Asistencia == null)
            {
                return NotFound();
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", Asistencia.IdAlumno);
            return View(Asistencia);
        }

        // POST: Attendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAsistencia,FechaAsistencia,IdAlumno,Estado,FechaCreacion,UserCreacion")] Asistencia Asistencia)
        {
            if (id != Asistencia.IdAsistencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Asistencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(Asistencia.IdAsistencia))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", Asistencia.IdAlumno);
            return View(Asistencia);
        }

        // GET: Attendance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var Asistencia = await _context.Asistencia
                .Include(a => a.IdAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.IdAsistencia == id);
            if (Asistencia == null)
            {
                return NotFound();
            }

            return View(Asistencia);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asistencia == null)
            {
                return Problem("Entity set 'Colegio1258Context.Asistencia'  is null.");
            }
            var Asistencia = await _context.Asistencia.FindAsync(id);
            if (Asistencia != null)
            {
                _context.Asistencia.Remove(Asistencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciaExists(int id)
        {
            return (_context.Asistencia?.Any(e => e.IdAsistencia == id)).GetValueOrDefault();
        }
        #endregion
    }
}
