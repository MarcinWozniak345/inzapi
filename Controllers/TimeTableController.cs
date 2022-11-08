using Inzynierka_API.Entities;
using Inzynierka_API.Models;
using Inzynierka_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inzynierka_API.Controllers
{
    [Authorize]
    [Route("api/timetable")]
    [ApiController]
    public class TImeTableController : ControllerBase
    {
        private readonly ITimeTableService _timetableService;
        public TImeTableController(ITimeTableService timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpGet("getmytimetables")]
        public ActionResult AddMedicament()
        {
            
            return Ok(_timetableService.GetMyTimeTables());
        }


    }
}
