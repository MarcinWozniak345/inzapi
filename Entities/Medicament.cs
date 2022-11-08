namespace Inzynierka_API.Entities
{
    public class Medicament
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public int NumberOfTablets { get; set; } //ilość tabletek/saszetek w opakowaniu
        public string Dose { get; set; } //dawka substancji
        public int DailyDosage { get; set;} // ile razy dziennie zarzywamy 

        public List<Time> Time { get; set; }
    }
}
