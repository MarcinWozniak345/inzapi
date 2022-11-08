using AutoMapper;
using Inzynierka_API.Entities;
using Inzynierka_API.Middleware;
using Inzynierka_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Inzynierka_API.Services
{
    public interface ITimeTableService
    {

        List<GetTimeTableDto> GetMyTimeTables();
    }

    public class TimeTableService : ITimeTableService
    {
        private readonly BazaDbContext _context;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public TimeTableService(BazaDbContext context, ILogger<ErrorHandlingMiddleware> logger, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public List<GetTimeTableDto> GetMyTimeTables()
        {
            //var mymedicaments = _context.Medicaments
            //   .Include(x => x.User)
            //   .Where(x => x.User.Id == (int)_userContextService.GetUserId);

            //return _mapper.Map<List<GetMedicamentDto>>(mymedicaments);

            var timetables = _context.TimeTables
                .Include(x => x.Medicament)
                .ThenInclude(x => x.User)
                .Where(x => x.Medicament.User.Id == (int)_userContextService.GetUserId);

                return _mapper.Map<List<GetTimeTableDto>>(timetables);


        }



    }
}
