using AutoMapper;
using Inzynierka_API.Entities;
using Inzynierka_API.Middleware;
using Inzynierka_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Inzynierka_API.Services
{

    public interface IMedicamentService
    {
        void AddMedicament(AddMedicamentDto dto);
        List<GetMedicamentDto> ShowFriendMedicaments(string login);
        List<GetMedicamentDto> ShowMyMedicaments();

    }
    public class MedicamentService : IMedicamentService
    {
        private readonly BazaDbContext _context;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public MedicamentService(BazaDbContext context, ILogger<ErrorHandlingMiddleware> logger, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public void AddMedicament(AddMedicamentDto dto)
        {
          
            var user = _context.Users.FirstOrDefault(u => u.Id == (int)_userContextService.GetUserId);
            _context.Attach(user);
            var newMedicament = new Medicament()
            {
                User = user,
                Name = dto.Name,
                NumberOfTablets = dto.NumberOfTablets,
                Dose = dto.Dose,
                DailyDosage = dto.DailyDosage,
                Time = dto.Time
            };
            _context.Medicaments.Add(newMedicament);
            _context.SaveChanges();
            _context.Attach(newMedicament);
            int tablets = dto.NumberOfTablets;
            
            int i = 0, k = 1;
            while (tablets != 0)
            {

                DateTime time = DateTime.Now.AddDays(k);
                for (int j = 0; j < dto.Time.Count(); j++)
                {
                    DateTime time2 = new DateTime(time.Year, time.Month, time.Day, dto.Time[j].Hour, dto.Time[j].Minute,0);
                    var newTimeTable = new TimeTable()
                    {
                        Medicament = newMedicament,
                        When = time2   
                    };
                    _context.TimeTables.Add(newTimeTable);
                    tablets--;
                    if (tablets == 0) break;
                }
                k++;

            }
            _context.SaveChanges();

        }

        public List<GetMedicamentDto> ShowFriendMedicaments(string login)
        {
            var friendmedicaments = _context.Medicaments
                .Include(x => x.User)
                .Where(x => x.User.Login == login);

            //var odwiedzoneDtos = _mapper.Map<List<ZwracaneOdwiedzoneDto>>(zapytanie);

            return _mapper.Map<List<GetMedicamentDto>>(friendmedicaments);
        }

        public List<GetMedicamentDto> ShowMyMedicaments()
        {
            var mymedicaments = _context.Medicaments
                .Include(x => x.User)
                .Where(x => x.User.Id == (int)_userContextService.GetUserId);

            return _mapper.Map<List<GetMedicamentDto>>(mymedicaments);
        }


    }
}
