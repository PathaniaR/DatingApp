using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApi.DTOs;
using DatingApi.Entities;

namespace DatingApi.Interfaces
{
    public interface ILikeRepository
    {
         Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
         Task<AppUser> GetUserWithLikes(int userId);
         Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
    }
}