using Inzynierka_API.Entities;
using Inzynierka_API.Models;
using Inzynierka_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inzynierka_API.Controllers
{
    [Authorize]
    [Route("api/medicament")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly IMedicamentService _medicamentService;
        public MedicamentController(IMedicamentService medicamentService)
        {
            _medicamentService = medicamentService;
        }

        [HttpPost("addmedicament")]
        public ActionResult AddMedicament([FromBody] AddMedicamentDto dto)
        {
            _medicamentService.AddMedicament(dto);
            return Ok();
        }
        [HttpGet("showfriendmedicaments")]
        public ActionResult ShowFriendMedicament([FromRoute] string login)
        {
            return Ok(_medicamentService.ShowFriendMedicaments(login));

        }
        [HttpGet("showmymedicaments")]
        public ActionResult ShowMyMedicament()
        {
            return Ok(_medicamentService.ShowMyMedicaments());

        }


    }
}
