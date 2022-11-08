using System.Security.Claims;

namespace Inzynierka_API.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccesor = httpContextAccessor;
        }


        public ClaimsPrincipal User => _httpContextAccesor.HttpContext?.User;
        public int? GetUserId => User is null ? null :(int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
