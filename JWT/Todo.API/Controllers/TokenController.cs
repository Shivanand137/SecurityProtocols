using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Todo.Entities;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string GetToken()
        {
            var tokenManagement = _configuration.GetSection("tokenManagement").Get<TokenManagement>();
            var secrets = _configuration.GetSection("secretSettings").Get<SecretManagement>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrets.TokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: tokenManagement.Issuer,
                audience: tokenManagement.Audience,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(tokenManagement.AccessExpiration)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}