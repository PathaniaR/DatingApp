using DatingApi.DTOs;
using DatingApi.Entities;
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
         Task<IEnumerable<MemberDto>> GetMembersAsync();
         Task<MemberDto> GetMemberAsync(string username);       
    }
}