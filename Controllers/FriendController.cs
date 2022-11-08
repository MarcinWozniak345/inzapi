using Inzynierka_API.Entities;
using Inzynierka_API.Models;
using Inzynierka_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inzynierka_API.Controllers
{
    [Route("api/friend")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;
        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }
        
        [HttpPost("addfriend")]
        public ActionResult AddFriend([FromBody] string name)
        {
            _friendService.SendInvitation(name);
            return Ok();
        }

        [HttpPut("confirmfriend")]
        public ActionResult ConfirmInvitation([FromBody] ConfirmInvitationDto dto)
        {
            _friendService.ConfirmInvitation(dto);
            return Ok();
        }
        [HttpGet("showinvitations")]
        public ActionResult ShowInvitations()
        {
            _friendService.ShowInvitations();
            return Ok();
        }

    }
}
