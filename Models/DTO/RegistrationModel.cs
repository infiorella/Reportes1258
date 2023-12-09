using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class RegistrationModel
    {
		[Required]
		public string Name { get; set; }
		[Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role {  get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}
