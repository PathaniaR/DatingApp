using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApi.DTOs;
using DatingApi.Entities;
using DatingApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Controllers
{
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users= await this._repository.GetMembersAsync();
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
           var userName=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           var user=await _repository.GetUserByNameAsync(userName);

           _mapper.Map(memberUpdateDto,user);
           _repository.Update(user);
           if(await _repository.SaveAllAsync()) return NoContent();
           return BadRequest("Failed to update a user");
        }
    }
}