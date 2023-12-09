using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Domain;
using Models.DTO;
using Repositories.Abstract;
using System.Security.Claims;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly Colegio1258Context context;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, Colegio1258Context context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public async Task<Status> LoginInAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Nombre de usuario inválido";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {

                status.StatusCode = 0;
                status.Message = "Contraseña inválida";
                return status;
            }


            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                //Data User
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>();

                //Data Personal
                var Personal = context.PersonalEducativos.Where(x => x.IdUsuario == user.Id)
                    .Select(i => new
                    {
                        i.IdPersonal,
                        i.Grado,
                        i.Seccion,
                        i.Rol
                    }).FirstOrDefault();

                //CLAIMS
                if (Personal != null)
                {
                    authClaims.Add(new Claim("IdPersonal", Personal.IdPersonal.ToString()));
                    authClaims.Add(new Claim("RolPersonal", Personal.Rol.ToString()));

                    if (Personal.Rol == 1)
                    {
                        authClaims.Add(new Claim("Grado", Personal.Grado.ToString()));
                        authClaims.Add(new Claim("Seccion", Personal.Seccion.ToString()));
                    }
                }

                await signInManager.SignInWithClaimsAsync(user, true, authClaims);

                status.StatusCode = 1;
                status.Message = "Inicio de Sesión exitoso";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "Usuario bloqueado";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error en inicio de sesión";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Nombre de usuario ya existe";
                return status;
            }

            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                RegistrationDate = DateTime.Now,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Creación fallida";
                return status;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(model.Role))
                await userManager.AddToRoleAsync(user, model.Role);

            status.StatusCode = 1;
            status.Message = "Usuario creado";
            return status;


        }


        public async Task<Status> ModificationAsync(RegistrationModel model)
        {
            var status = new Status();
            //Search the user
            var user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Usuario no existe";
                return status;
            }

            //Update user
            var result = await UpdateUser(user, model.Username, model.Name);

            //Update Rol
            var rol = await UpdateUserRole(user, model.Role);

            //Update Password
            var password = await UpdatePassword(user, model.Password);


            if (result.StatusCode != 1 && rol.StatusCode != 1 && password.StatusCode != 1)
            {
                status.StatusCode = 0;
                status.Message = "Modificación Fallida";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "Usuario Modificado";
            return status;


        }

        public async Task<Status> UpdateUser(ApplicationUser user, string Username, string Name)
        {
            var status = new Status();

            user.UserName = Username;
            user.Name = Name;
            user.RegistrationDate = user.RegistrationDate;
            user.ModificationDate = DateTime.Now;

            //Update user
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                status.StatusCode = 1;
                return status;
            }
            else
            {
                status.StatusCode = 0;
                return status;
            }
        }


        public async Task<Status> UpdatePassword(ApplicationUser user, string Password)
        {
            var status = new Status();
            //Check if password has text
            if (!string.IsNullOrWhiteSpace(Password))
            {
                var result = await userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    result = await userManager.AddPasswordAsync(user, Password);
                    if (result.Succeeded)
                    {
                        status.StatusCode = 1;
                        return status;
                    }
                    else
                    {
                        status.StatusCode = 0;
                        return status;
                    }
                }
                else
                {
                    status.StatusCode = 0;
                    return status;
                }
            }
            status.StatusCode = 1;
            return status;
        }


        public async Task<Status> UpdateUserRole(ApplicationUser user, string newRole)
        {
            var status = new Status();
            var oldRole = userManager.GetRolesAsync(user).Result.FirstOrDefault();

            //Check if it's the same as old
            if (oldRole != newRole)
            {
                var result = await userManager.RemoveFromRoleAsync(user, oldRole);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, newRole);
                    if (result.Succeeded)
                    {
                        status.StatusCode = 1;
                        return status;
                    }
                    else
                    {
                        status.StatusCode = 0;
                        return status;
                    }
                }
                else
                {
                    status.StatusCode = 0;
                    return status;
                }
            }
            status.StatusCode = 1;
            return status;
        }


        public async Task<Status> DeleteConfirmed(string id)
        {
            var status = new Status();
            // Find the user account to delete.
            var user = await userManager.FindByIdAsync(id);

            // Delete the user account.
            var result = await userManager.DeleteAsync(user);

            // Delete any related data.
            if (result.Succeeded)
            {
                try
                {
                    // Delete the user's roles.
                    var roles = await userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        await userManager.RemoveFromRoleAsync(user, role);
                    }

                    // Delete the user's claims.
                    var claims = await userManager.GetClaimsAsync(user);
                    foreach (var claim in claims)
                    {
                        await userManager.RemoveClaimAsync(user, claim);
                    }

                    // Delete the user's logins.
                    var logins = await userManager.GetLoginsAsync(user);
                    foreach (var login in logins)
                    {
                        await userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    }
                    status.StatusCode = 1;
                    return status;
                }
                catch (Exception ex)
                {
                    status.StatusCode = 0;
                    return status;
                }

            }
            else
            {
                status.StatusCode = 0;
                return status;
            }

        }
    }
}
