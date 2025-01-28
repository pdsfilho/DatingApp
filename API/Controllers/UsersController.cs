using System;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        //var users = context.Users.ToList();
        var users = await userRepository.GetUsersAsync();
        
        var usersToReturn = mapper.Map<IEnumerable<MemberDTO>>(users);

        //Collections tend to not accept a return users, for some reason. So Ok was used.
        return Ok(usersToReturn);
    }

    
    [HttpGet("{username}")] // /api/users/2
    public async Task <ActionResult<MemberDTO>> GetUser(string username)
    {
        //var user = context.Users.Find(id);
        
        var user = await userRepository.GetUserByUserNameAsync(username);

        if (user == null) return NotFound();
        
        return mapper.Map<MemberDTO>(user);
    }

}
