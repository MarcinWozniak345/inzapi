namespace Inzynierka_API.Entities
{
    public class Friend
    {
        public int Id { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }

        public bool Confirmed { get; set; }

    }
}
