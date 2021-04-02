using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApi.Data;
using DatingApi.DTOs;
using DatingApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DatingDataContext _context;
        public AccountController(DatingDataContext context)
        {
            _context=context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register([FromBody] RegisterDto dto)
        {
           using var hmac=new HMACSHA512();
           if(await UserExists(dto.Username))
           {
             return BadRequest("User Already Exists");
           }
           var user=new AppUser()  
           {
             UserName= dto.Username.ToLower(),
             PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
             PasswordSalt=hmac.Key
           };

           _context.AppUsers.Add(user);
           await _context.SaveChangesAsync();
           return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login([FromBody] RegisterDto dto)
        {
             var user = await _context.AppUsers
                                   .SingleOrDefaultAsync<AppUser>(z=>z.UserName == dto.Username);

              if(user==null)
              {
                 return Unauthorized("Invalid User");
              }

             using var hmac=new HMACSHA512(user.PasswordSalt);
             var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
             for(int i=0;i<=computedHash.Length;i++)
             {
                if(computedHash[i]!=user.PasswordHash[i])
                {
                  return Unauthorized("Invalid Password");
                }
             }

             return user;
        }


        private async Task<bool> UserExists(string userName)
        {
           return await _context.AppUsers.AnyAsync(x=>x.UserName==userName.ToLower());
        }
    }
}