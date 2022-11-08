using AutoMapper;
using Inzynierka_API.Entities;
using Inzynierka_API.Middleware;
using Inzynierka_API.Models;

namespace Inzynierka_API.Services
{

    public interface IFriendService
    {
        void SendInvitation(string name);
        List<ShowInvitationsDto> ShowInvitations();
        void ConfirmInvitation(ConfirmInvitationDto dto);

    }
    public class FriendService : IFriendService
    {
        private readonly BazaDbContext _context;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public FriendService(BazaDbContext context, ILogger<ErrorHandlingMiddleware> logger, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public void SendInvitation(string name)
        {          
            var user1 = _context.Users.FirstOrDefault(u => u.Id == (int)_userContextService.GetUserId);
            _context.Attach(user1);
            var user2 = _context.Users.FirstOrDefault(u => u.Login == name);
            _context.Attach(user2);
            var newfriend = new Friend()
            {
                User1 = user1,
                User2 = user2,
                Confirmed = false

            };
            _context.Friends.Add(newfriend);
            _context.SaveChanges();
        }
        public void ConfirmInvitation(ConfirmInvitationDto dto)
        {
            var friend = _context.Friends.FirstOrDefault(f => f.User1.Login == dto.name);
            if (dto.ifconfirm == true)
            {
                friend.Confirmed = true;
            }
            else
            {
                _context.Remove(friend);
            }
            _context.SaveChanges();
        }

        public List<ShowInvitationsDto> ShowInvitations()
        {
            var invitations = _context.Friends.Where(u => u.User2.Id == (int)_userContextService.GetUserId);

            return _mapper.Map<List<ShowInvitationsDto>>(invitations);
        }

    }
}
