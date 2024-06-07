using DataAccessLayer.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DataAccessLayer.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Repository;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private readonly AppDbContext _cIDbContext;

        public LoginController( AppDbContext cIDbContext,IConfiguration config)
        {
            _config = config;
            _cIDbContext = cIDbContext;
        }

        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] UserLogin loginModel)
        {
            var user = await _cIDbContext.Users.SingleOrDefaultAsync(x => x.Username == loginModel.Username);

            if (user == null || user.Password != loginModel.Password)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(UserInfo user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //public IActionResult Login([FromBody] UserLogin userLogin)
        //{
        //    var user = Authenticate(userLogin);

        //    if (user != null)
        //    {
        //        var token = Generate(user);
        //        return Ok("Welcome: " + user.GivenName + "Your JWT Token: " + token);
        //        //return Ok("Welcome: " + user.GivenName);
        //    }

        //    return NotFound("User not found");
        //}

        //private string Generate(UserInfo user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //         new Claim(ClaimTypes.NameIdentifier, user.Username),
        //         new Claim(ClaimTypes.Email, user.EmailAddress),
        //         new Claim(ClaimTypes.GivenName, user.GivenName),
        //         new Claim(ClaimTypes.Surname, user.Surname),
        //         new Claim(ClaimTypes.Role, user.Role)
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Audience"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(15),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private UserInfo Authenticate(UserLogin userLogin)
        //{
        //    var currentUser = DALStatics.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

        //    if (currentUser != null)
        //    {
        //        return currentUser;
        //    }

        //    return null;
        //}
    }
}
