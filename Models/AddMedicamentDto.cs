using Inzynierka_API.Entities;

namespace Inzynierka_API.Models
{
    public class AddMedicamentDto
    {
        public string Name { get; set; }
        public int NumberOfTablets { get; set; } //ilość tabletek/saszetek w opakowaniu
        public string Dose { get; set; } //dawka substancji
        public int DailyDosage { get; set; } // ile razy dziennie zarzywamy 

        public List<Time> Time { get; set; }
    }
}
