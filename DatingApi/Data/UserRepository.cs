using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatingDataContext _context;
         private readonly IMapper _mapper;
        public UserRepository(DatingDataContext context,IMapper mapper)
        {
            this._context=context;
            this._mapper=mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
           return  await _context.AppUsers
                    .Where(x=>x.UserName == username)
                    .ProjectTo<MemberDto>(this._mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return  await _context.AppUsers
                    .ProjectTo<MemberDto>(this._mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task<AppUser> GetUserByNameAsync(string userName)
        {
            return await _context.AppUsers
                                .Include(p => p.Photos)
                                .SingleOrDefaultAsync(x=>x.UserName==userName);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
           return await _context.AppUsers
                                .Include(p => p.Photos)
                                .ToListAsync<AppUser>();
        }

        public async Task<bool> SaveAllAsync()
        {
          return await _context.SaveChangesAsync()>0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State=EntityState.Modified;
        }
    }
}