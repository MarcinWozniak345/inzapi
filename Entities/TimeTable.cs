namespace Inzynierka_API.Entities
{
    public class TimeTable
    {
        public int Id { get; set; }
        public Medicament Medicament { get; set; }
        public DateTime When { get; set; }
    }
}
