namespace Models.Views
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string? Password {
            get => "";
            set=> Password = value; 
        }
        public DateTime? Creacion { get; set; }
        public DateTime? Modificacion { get; set; }

    }
}
