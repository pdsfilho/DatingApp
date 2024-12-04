using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        //var users = context.Users.ToList();
        var users = await context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id}")]
    public async Task <ActionResult<AppUser>> GetUser(int id)
    {
        //var user = context.Users.Find(id);
        
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound();
        
        return user;
    }

}
