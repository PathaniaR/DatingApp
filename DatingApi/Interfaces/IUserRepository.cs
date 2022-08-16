using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApi.Interfaces
{
    public interface IUserRepository
    {
         void Update(AppUser user);
         Task<bool> SaveAllAsync();
         Task<IEnumerable<AppUser>> GetUsersAsync();
         Task<AppUser> GetUserByIdAsync(int id);
         Task<AppUser> GetUserByNameAsync(string userName);
         Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
         Task<MemberDto> GetMemberAsync(string username);       
    }
}