using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatingApi.Entities;
using DatingApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims=new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
            };

            var credentials=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new SecurityTokenDescriptor
            {
               Subject=new ClaimsIdentity(claims),
               Expires=DateTime.Now.AddDays(7),
               SigningCredentials=credentials
            };

            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}