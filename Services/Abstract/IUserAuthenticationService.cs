using Models.Domain;
using Models.DTO;

namespace Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginInAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegistrationAsync(RegistrationModel model);
        Task<Status> ModificationAsync(RegistrationModel model);
        Task<Status> UpdateUser(ApplicationUser user, string Username, string Name);
        Task<Status> UpdatePassword(ApplicationUser user, string Password);
        Task<Status> UpdateUserRole(ApplicationUser user, string newRole);
        Task<Status> DeleteConfirmed(string id);
    }
}
