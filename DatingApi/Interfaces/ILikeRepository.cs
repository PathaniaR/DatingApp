using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Helpers;

namespace DatingApi.Interfaces
{
    public interface ILikeRepository
    {
         Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
         Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}