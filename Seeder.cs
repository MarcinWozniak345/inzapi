using Inzynierka_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inzynierka_API
{
    public class Seeder
    {
        private readonly BazaDbContext _dbcontext;

        public Seeder(BazaDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public void Seed()
        {
            if(_dbcontext.Database.CanConnect())
            {
               var pendingMigrations = _dbcontext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbcontext.Database.Migrate();
                }
            }
        }
    }
}
