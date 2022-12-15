using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PYPJ.Interfaces;
using PYPJ.Models;
using PYPJ.Models.CustomModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PYPJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly JWTSetting setting;
        private readonly IRefreshTokenGenerate _tokenGenerator;
        private readonly IConfiguration _configuration;
        private readonly PypjContext _context;

        public AuthController(IRefreshTokenGenerate tokenGenerator, IConfiguration configuration, PypjContext context)
        {
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
            _context = context;
        }

        [Route("Authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var _user = _context.TblUsers.FirstOrDefault(o => o.Name == model.UserName && o.Password == model.Password && o.IsActive == true);
            if (_user == null)
                return Unauthorized();

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSetting").GetSection("securitykey").Value);
            var tokenIssuer = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSetting").GetSection("securitykey").Value);
            var tokenAudience = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSetting").GetSection("securitykey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, _user.UserId.ToString()),
                        new Claim(ClaimTypes.Role, _user.Role.ToString())

                    }
                ),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            tokenResponse.JWTToken = finaltoken;
            tokenResponse.RefreshToken = _tokenGenerator.GenerateToken(_user.UserId);

            return Ok(tokenResponse);
        }

        [NonAction]
        public TokenResponse Authenticate(Guid UserId, Claim[] claims)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var tokenkey = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSetting").GetSection("securitykey").Value);
            var tokenhandler = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)

                );
            tokenResponse.JWTToken = new JwtSecurityTokenHandler().WriteToken(tokenhandler);
            tokenResponse.RefreshToken = _tokenGenerator.GenerateToken(UserId);

            return tokenResponse;
        }

        [Route("Refresh")]
        [HttpPost]
        public IActionResult Refresh([FromBody] TokenResponse token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token.JWTToken);
            //var username = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            var UserId = Guid.Parse(securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);


            //var username = principal.Identity.Name;
            var _reftable = _context.TblRefreshTokens.FirstOrDefault(o => o.UserId == UserId && o.RefreshToken == token.RefreshToken);
            if (_reftable == null)
            {
                return Unauthorized();
            }
            TokenResponse _result = Authenticate(UserId, securityToken.Claims.ToArray());
            return Ok(_result);
        }
    }
}
