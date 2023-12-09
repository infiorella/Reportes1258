using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Domain;
using Models.DTO;
using Models.Views;
using OfficeOpenXml;
using Repositories.Abstract;
using SkiaSharp;

namespace Colegio1258.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsersController : Controller
    {
        public readonly IUserAuthenticationService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _context;

        public UsersController(IUserAuthenticationService service, UserManager<ApplicationUser> usermanager, DatabaseContext contextid)
        {
            _service = service;
            _userManager = usermanager;
            _context = contextid;
        }


        //Inde
        public async Task<IActionResult> Index()
        {
            var result = _context.Users
            .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
            .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
            .Select(c => new UsersViewModel()
            {
                Id = c.ur.u.Id,
                Username = c.ur.u.UserName,
                Name = c.ur.u.Name,
                Creacion = c.ur.u.RegistrationDate,
                Modificacion = c.ur.u.ModificationDate,
                Role = c.r.Name
            }).ToList();

            return View(result);
        }


        //Details
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            UsersViewModel model = new UsersViewModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
                Creacion = user.RegistrationDate,
                Modificacion = user.ModificationDate
            };
            return PartialView("Details", model);
        }

        //Create
        public IActionResult Create()
        {
            RegistrationModel per = new RegistrationModel();
            return PartialView("Create", per);
        }

        // POST: Users/Create                
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            TempData["Status"] = result.StatusCode;
            return RedirectToAction(nameof(Index));
        }

        // Edit
        public IActionResult Edit(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            RegistrationModel model = new RegistrationModel()
            {
                Username = user.UserName,
                Name = user.Name,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
                RegistrationDate = user.RegistrationDate.GetValueOrDefault()
            };
            return PartialView("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Modification(RegistrationModel model)
        {
            // Save the changes.
            var result = await _service.ModificationAsync(model);
            TempData["msg"] = result.Message;
            TempData["status"] = result.StatusCode;
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var status = await _service.DeleteConfirmed(id);
            if (status.StatusCode == 1)
            {
                return Json(new { success = true, message = "Registro Eliminado" });
            }
            return Json(new { success = false, message = "Problemas para eliminar registro" });
        }
    }
}
