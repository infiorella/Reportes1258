using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Colegio1258.Repositories.Implementation
{
    internal static class ClaimsPrincipalExtentions
    {
        internal static int Grado()
        {
            var claimPrincipal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var Grado = claimPrincipal.FindFirst("Grado");

            if (Grado != null)
            {
                return Convert.ToInt32(Grado.Value);
            }
            else
            {
                return 0;
            }
        }

        public static int IdPersonal()
        {
            var claimPrincipal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var IdPersonal = claimPrincipal.FindFirst("IdPersonal");

            if (IdPersonal != null)
            {
                return Convert.ToInt32(IdPersonal.Value);
            }
            else
            {
                return 0;
            }
        }

        public static int RolPersonal()
        {
            var claimPrincipal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var RolPersonal = claimPrincipal.FindFirst("RolPersonal");

            if (RolPersonal != null)
            {
                return Convert.ToInt32(RolPersonal.Value);
            }
            else
            {
                return 0;
            }
        }

        public static string Seccion()
        {
            var claimPrincipal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var Seccion = claimPrincipal.FindFirst("Seccion");

            if (Seccion != null)
            {
                return Seccion.Value;
            }
            else
            {
                return "";
            }
        }
    }
}
