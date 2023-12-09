using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Repositories.Abstract;

namespace Controllers
{
    public class UserAuthenticationController : Controller
    {
        public readonly IUserAuthenticationService _service;

        public UserAuthenticationController(IUserAuthenticationService service)
        {
            _service = service;
        }


        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            model.RegistrationDate = DateTime.Now;
            if (!ModelState.IsValid)
                return View(model);

            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;

            return RedirectToAction(nameof(Registration));
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.LoginInAsync(model);

            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }


        public async Task<IActionResult> Usuario1()
        {
            var model = new RegistrationModel
            {
                Username = "",
                Name = "",
                Password = "",
                RegistrationDate = DateTime.Now
            };

            model.Role = "Docente";
            var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }

        public async Task<IActionResult> reg1()
        {
            var model = new RegistrationModel { Username = "CValenzuela", Name = "Cecilia Jannet Valenzuela Villajuan", Password = "VaCe40754377@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg2()
        {
            var model = new RegistrationModel { Username = "ELozano", Name = "Eliana Elizabeth Lozano Calixto", Password = "LoEl41726506@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg3()
        {
            var model = new RegistrationModel { Username = "ETacuri", Name = "Elizabeth Tacuri Casaño", Password = "TaEl40392624@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);

            return Ok(result);
        }
        public async Task<IActionResult> reg4()
        {
            var model = new RegistrationModel { Username = "VYauri", Name = "Vilma Yauri Samaniego", Password = "YaVi19866423@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);

            return Ok(result);
        }

        public async Task<IActionResult> reg5()
        {
            var model = new RegistrationModel { Username = "HCardenas", Name = "Heldy Maritza Cárdenas Ramos", Password = "CáHe10599072@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg6()
        {
            var model = new RegistrationModel { Username = "WSalvatierra", Name = "Willian Salvatierra Celis", Password = "SaWi40276331@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg7()
        {
            var model = new RegistrationModel { Username = "ACasimiro", Name = "Angela Erika Casimiro Egoavil", Password = "CaAn40705525@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg8()
        {
            var model = new RegistrationModel { Username = "NRamirez", Name = "Nelidamaribel Ramirez Estrella", Password = "RaNe43616150@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg9()
        {
            var model = new RegistrationModel { Username = "EPaucar", Name = "Elizabeth Inés Victoria Paucar Hichcas", Password = "PaEl48355278@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg10()
        {
            var model = new RegistrationModel { Username = "MPalomino", Name = "Miguel Angel Palomino Mayaute", Password = "PaMi9355990@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg11()
        {
            var model = new RegistrationModel { Username = "DFernandez", Name = "Denisse Gavy Fernández Miranda", Password = "FeDe41524792@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg12()
        {
            var model = new RegistrationModel { Username = "CMartinez", Name = "Charito Violeta Martinez Flores", Password = "MaCh10726049@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg13()
        {
            var model = new RegistrationModel { Username = "CGanto", Name = "Carmen Rosa Ganto Robles", Password = "GaCa4044421@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg14()
        {
            var model = new RegistrationModel { Username = "AAvelino", Name = "Aymmy Jhovany Avelino Gago", Password = "AvAy40181697@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg15()
        {
            var model = new RegistrationModel { Username = "OLozano", Name = "Oswaldo Victor Lozano Riveros", Password = "LoOs8145046@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg16()
        {
            var model = new RegistrationModel { Username = "GRamos", Name = "Gloria Beatriz Ramos Lengua", Password = "RaGl21475033@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg17()
        {
            var model = new RegistrationModel { Username = "DVillavicencio", Name = "Dora Beatriz Villavicencio Leon", Password = "ViDo4068300@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg18()
        {
            var model = new RegistrationModel { Username = "RVasquez", Name = "Rosmery Luz Vasquez Avalos", Password = "VaRo48384150@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg19()
        {
            var model = new RegistrationModel { Username = "FHerrera", Name = "Fanio Porfirio Herrera Sanchez", Password = "HeFa6808574@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg20()
        {
            var model = new RegistrationModel { Username = "YGuinea", Name = "Yesenia Guinea Asturay", Password = "GuYe44797927@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg21()
        {
            var model = new RegistrationModel { Username = "RSulca", Name = "Reyna Sulca Gamboa", Password = "SuRe10600886@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg22()
        {
            var model = new RegistrationModel { Username = "SChilon", Name = "Samuel Chilon Ishpilco", Password = "ChSa10296888@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg23()
        {
            var model = new RegistrationModel { Username = "VGarcia", Name = "Victor Hugo Garcia Torres", Password = "GaVi25809234@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg24()
        {
            var model = new RegistrationModel { Username = "ACalizaya", Name = "Ana Sofia Calizaya Jimenez", Password = "CaAn9065305@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg25()
        {
            var model = new RegistrationModel { Username = "JChoque", Name = "Jaime Fernando Choque Medina", Password = "ChJa22291146@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg26()
        {
            var model = new RegistrationModel { Username = "MCossio", Name = "Miguel Ángel Cossio Otivo", Password = "CoMi9952422@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg27()
        {
            var model = new RegistrationModel { Username = "JCcopa", Name = "José Antonio Ccopa Lorenzo", Password = "CcJo10171355@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg28()
        {
            var model = new RegistrationModel { Username = "EGamonal", Name = "Elmer Gamonal Castillo", Password = "GaEl16170721@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg29()
        {
            var model = new RegistrationModel { Username = "SChancasanampa", Name = "Smith Jonathan Chancasanampa Gonzales", Password = "ChSm44093660@" }; model.Role = "Docente"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg30()
        {
            var model = new RegistrationModel { Username = "CVillarroel", Name = "Carlos Enrique Villarroel Espinoza", Password = "ViCa7666198@" }; model.Role = "Directivo"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg31()
        {
            var model = new RegistrationModel { Username = "FVasquez", Name = "Fernando Melanio Vasquez Diaz", Password = "VaFe10493486@" }; model.Role = "Directivo"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> reg32()
        {
            var model = new RegistrationModel { Username = "avasquez", Name = "Ana Fiorella Vasquez Garcia", Password = "Av72783220@" }; model.Role = "Administrador"; var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }

    }
}
