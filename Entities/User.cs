namespace Inzynierka_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool IfAdmin { get; set; }

    }
}
