using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Extensions;
using DatingApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DatingDataContext _context;
        public LikeRepository(DatingDataContext dataContext)
        {
           _context = dataContext; 
        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId,likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
           var users = _context.AppUsers.OrderBy(x=>x.UserName).AsQueryable();
           var likes = _context.Likes.AsQueryable();
           if(predicate == "liked")
           {
             likes = likes.Where(x=>x.SourceUserId == userId);
             users = likes.Select(x=>x.LikedUser);
           }
           if(predicate == "likedBy"){
             likes = likes.Where(x=>x.LikedUserId == userId);
             users = likes.Select(x=>x.SourceUser);
           }

           return await users.Select(x=> new LikeDto{
               Username = x.UserName,
               KnownAs = x.KnownAs,
               Age = x.DateOfBirth.CalculateAge(),
               PhotoUrl = x.Photos.FirstOrDefault(x=>x.IsMain).Url,
               City = x.City,
               Id = x.Id
           }).ToListAsync();
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.AppUsers.Include(x=>x.LikedUsers).FirstOrDefaultAsync(x=>x.Id == userId);
        }
    }
}