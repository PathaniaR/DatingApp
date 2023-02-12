using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApi.Authorization;
using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Extensions;
using DatingApi.Helpers;
using DatingApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Controllers
{
    //[Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repository,IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        [HttpGet]
        //[HasPermission(Permission.ReadMember)]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            //var user = await _repository.GetUserByNameAsync(User.GetUserName());
            //userParams.CurrentUsername = user.UserName;
            //if (string.IsNullOrEmpty(userParams.Gender))
            //{
            //    userParams.Gender = user.Gender == "male" ? "female" : "male";
            //}
            var users= await this._repository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages);
            return Ok(users);
        }
       [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user= await _repository.GetMemberAsync(username);
            return user;
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userName = User.GetUserName();
            var user = await _repository.GetUserByNameAsync(userName);

            _mapper.Map(memberUpdateDto, user);
            _repository.Update(user);
            if (await _repository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update a user");
        }
    }
}