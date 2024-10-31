using LMSOnline.Data;
using LMSOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMSOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly AccountService accountService;
        public AccountController(IConfiguration configuration,AccountService accountService)
        {
            this.configuration = configuration;
            this.accountService = accountService;
        }
        [HttpPut("UpdatePassword/{id}")]
        [Authorize(Policy ="LeaderShip")]
        public IActionResult UpdatePassword(int id,[FromBody]ChangPasswordDTO changPasswordDTO)
        {
            try
            {
                return Ok(accountService.ChangePassword(id, changPasswordDTO));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("UpdateAvatar/{id}")]
            [Authorize(Policy = "LeaderShip")]
        public IActionResult UpdateAvatar(int id, [FromForm]UpdateAvatar avatar)
        {
            try
            {
                return Ok(accountService.UpdateAvatar(id, avatar));
            }
            catch
            {
                return BadRequest();
            }
        }
        private string GenerateJWTToken(AccountDTO user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("FullName",user.FullName),
                new Claim(ClaimTypes.Role,user.Role)

            };
            var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet("ProfileAccount/{id}")]
        [Authorize(Policy = "LeaderShip")]
        public IActionResult ProfileAccount(int id)
        {
            try
            {
                return Ok(accountService.ProfileAccount(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var account=accountService.Login(loginDTO);
            if(account == null)
            {
                return Unauthorized();
            }
            var token=GenerateJWTToken(account);
            return Ok(new
            {
                token,
                claims = new
                {
                    account.Id,
                    account.Username,
                    account.Email,
                    account.FullName,
                    Role = account.Role,
                    account.Phone,
                    account.Avatar,
                },
                message = "Login Sucess"
            });
        }
    }
}
