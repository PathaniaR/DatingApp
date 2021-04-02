using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApi.Data;
using DatingApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DatingDataContext _context;
        public UsersController(DatingDataContext datingDataContext)
        {
            _context=datingDataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users= await _context.AppUsers.ToListAsync();   
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user= await _context.AppUsers.FindAsync(id);
            return user;
        }
    }
}