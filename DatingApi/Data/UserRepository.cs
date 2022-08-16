using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Helpers;
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

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query= _context.AppUsers.AsQueryable();
            //query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(x => x.Gender == userParams.Gender);

            var minDob = System.DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = System.DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(x => x.LastActive)
            };
            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>
            (_mapper.ConfigurationProvider).AsNoTracking(),userParams.PageNumber,userParams.PageSize);

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