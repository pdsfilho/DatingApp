using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")] //account/register
    public async Task<ActionResult<AppUser>> Register(string username, string password)
    {
        //Using the Hash to generate a new password. 
        //The "using" statement already disposes of the instance after is used.
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
}
