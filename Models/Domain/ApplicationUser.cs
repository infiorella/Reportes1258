using Microsoft.AspNetCore.Identity;

namespace Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string? Name { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public virtual ICollection<PersonalEducativo> PersonalEducativos { get; set; } = new List<PersonalEducativo>();
    }
}
