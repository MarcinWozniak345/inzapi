using Inzynierka_API.Entities;
using Inzynierka_API.Exceptions;
using Inzynierka_API.Middleware;
using Inzynierka_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;

namespace Inzynierka_API.Services
{
    public interface ILoginService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        //void ConfirmUser(ConfirmUserDto dto);
    }

    public class LoginService : ILoginService
    {
        private readonly BazaDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly AuthenticationSettings _authenticationSettings;
        public LoginService(BazaDbContext context, IPasswordHasher<User> passwordHasher, ILogger<ErrorHandlingMiddleware> logger, AuthenticationSettings authenticationSettings)
        {
            _context = context; 
            _passwordHasher = passwordHasher;
            _logger = logger;
            _authenticationSettings = authenticationSettings;          
        }
        public void RegisterUser(RegisterUserDto dto)
        {

            var newUser = new User()
            {
                Email = dto.Email,
                Login = dto.Login,
                IfAdmin = dto.CzyAdmin,
                Active = true,
            };
            var HashOdHasla = _passwordHasher.HashPassword(newUser, dto.HasloHash);
            newUser.Password = HashOdHasla;

            _context.Users.Add(newUser);
            _context.SaveChanges();

        }
        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u =>  u.Login == dto.Login);
            if (user is null)
            {
                throw new BadRequestException("niepoprawny login lub haslo");
            }
            if (user.Active == false)
            {
                throw new BadRequestException("konto nie zostalo jeszcze aktywowane");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("niepoprawny login lub haslo");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Login} {user.Email}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                                             _authenticationSettings.JwtIssuer,
                                             claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        //public void ConfirmUser(ConfirmUserDto dto)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
        //    if (user is null)
        //    {
        //        throw new BadRequestException("niepoprawne dane");
        //    }
        //    if (user.Active == true)
        //    {
        //        throw new BadRequestException("konto zostało juz aktywowane");
        //    }
            
        //        user.Active = true;
        //        _context.SaveChanges();
            
        //}

    }

 
}
