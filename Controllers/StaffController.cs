using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Domain;
using OfficeOpenXml;

namespace Colegio1258.Controllers
{
    public class StaffController : Controller
    {
        private readonly Colegio1258Context _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _identityContext;

        public StaffController(Colegio1258Context context, DatabaseContext identityContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _identityContext = identityContext;
            _userManager = userManager;
        }

        #region "Views"
        //Index
        [Authorize(Roles = "Administrador, Directivo")]
        public async Task<IActionResult> Index()
        {
            return _context.PersonalEducativos != null ?
                        View(await _context.PersonalEducativos.ToListAsync()) :
                        Problem("Error. No existen registros");
        }


        //Details
        [Authorize(Roles = "Administrador, Directivo")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonalEducativos == null)
            {
                return NotFound();
            }

            var user = await _context.PersonalEducativos
                .FirstOrDefaultAsync(m => m.IdPersonal == id);
            if (user == null)
            {
                return NotFound();
            }
            return PartialView("Details", user);
        }

        // GET: Staff/Edit/5
        [Authorize(Roles = "Administrador, Directivo")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonalEducativos == null)
            {
                return NotFound();
            }

            var personalEducativo = await _context.PersonalEducativos.FindAsync(id);
            if (personalEducativo == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_identityContext.Users.ToList(), "Id", "UserName", personalEducativo.IdUsuario);
            return View(personalEducativo);
        }

        //Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            PersonalEducativo per = new PersonalEducativo();
            ViewData["IdUsuario"] = new SelectList(_identityContext.Users.ToList(), "Id", "UserName");
            return View(per);
        }


        [Authorize(Roles = "Administrador")]
        public IActionResult ImportData()
        {     
            return View();
        }

        #endregion

        #region "Post Methods"
        //Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonalEducativo personalEducativo)
        {
            var user = ClaimsPrincipal.Current.Identity.Name;
            personalEducativo.FechaCreacion = DateTime.Now;
            personalEducativo.UserCreacion = user;

            if (ModelState.IsValid)
            {
                _context.Add(personalEducativo);
                await _context.SaveChangesAsync();
                TempData["msg"] = "Registro Creado";
                TempData["status"] = 1;
                return RedirectToAction(nameof(Index));
            }
            TempData["msg"] = "Verifique los datos ingresados";
            TempData["status"] = 0;
            return View(personalEducativo);
        }

        //Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonalEducativo personalEducativo)
        {
            var user = ClaimsPrincipal.Current.Identity.Name;
            personalEducativo.FechaModificacion = DateTime.Now;
            personalEducativo.UserModificacion = user;
            //personalEducativo.IdUsuarioNavigation= await _userManager.FindByIdAsync(personalEducativo.IdUsuario);

            try
            {
                if (id == personalEducativo.IdPersonal)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Update(personalEducativo);
                        await _context.SaveChangesAsync();
                        TempData["msg"] = "Registro Modificado";
                        TempData["status"] = 1;
                    }
                }
                else
                {
                    TempData["msg"] = "El registro no pudo ser modificado";
                    TempData["status"] = 0;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                TempData["status"] = 0;
            }
            return RedirectToAction(nameof(Edit), new { id = id });
        }

        // POST: Staff/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.PersonalEducativos == null)
            {
                return Problem("No existe datos relacioandos al Personal de la I.E.");
            }
            var personalEducativo = await _context.PersonalEducativos.FindAsync(id);
            try
            {
                if (personalEducativo != null)
                {
                    personalEducativo.IdEstado = 0;
                    _context.PersonalEducativos.Attach(personalEducativo);
                    _context.Entry(personalEducativo).Property(X => X.IdEstado).IsModified = true;
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Registro Eliminado" });
            } catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message});
            }
           
        }

        #endregion

        #region "Extras"
        private bool PersonalEducativoExists(int id)
        {
            return (_context.PersonalEducativos?.Any(e => e.IdPersonal == id)).GetValueOrDefault();
        }

        public async Task<List<PersonalEducativo>> Import(IFormFile file)
        {
            var list = new List<PersonalEducativo>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row >= rowCount; row++)
                    {
                        list.Add(new PersonalEducativo
                        {
                            Grado = Convert.ToInt32(Regex.Match(worksheet.Cells[row, 2].Value.ToString(), @"\d+").Value),
                            Seccion = worksheet.Cells[row, 2].Value.ToString().Substring(1).Trim(),
                            Apellidos = worksheet.Cells[row, 3].Value.ToString().Substring(1).Trim().Split(',').FirstOrDefault(),
                            Nombres = worksheet.Cells[row, 3].Value.ToString().Substring(1).Trim().Split(',').LastOrDefault(),
                            Dni = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            Telefono = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            CorreoElectronico = worksheet.Cells[row, 6].Value.ToString().Trim(),
                        }); ;
                    }
                    return list;
                }
            }
        }
        #endregion
    }
}
