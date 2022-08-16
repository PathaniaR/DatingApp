using System.Threading.Tasks;
using DatingApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApi.Controllers
{
    [Authorize]
    public class LikeController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;
        public LikeController(IUserRepository userRepository , ILikeRepository likeRepository)
        {
            _userRepository = userRepository;
            _likeRepository = likeRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string likedUserName, int sourceUserId)
        {
          var likedUser = await _userRepository.GetUserByNameAsync(likedUserName);
          var sourceUser = await _likeRepository.GetUserWithLikes(sourceUserId);

          if(likedUser == null){
              return NotFound();
          }

          if(sourceUser.UserName == likedUserName){
              return BadRequest("You cannot like yourself");
          }

          var userLike = await _likeRepository.GetUserLike(sourceUserId,likedUser.Id);
          if(userLike !=null)  return BadRequest("You already liked this user");

          userLike = new Entities.UserLike()
          {
              SourceUserId = sourceUserId,
              LikedUserId = likedUser.Id
          };

          sourceUser.LikedUsers.Add(userLike);
          if(await _userRepository.SaveAllAsync()){
              return Ok();
          }
          else{
              return BadRequest("Failed to like user");
          }
        }
    }
}