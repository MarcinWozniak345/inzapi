using Microsoft.EntityFrameworkCore;

namespace Inzynierka_API
{
    //[Owned]
    public class Time
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
