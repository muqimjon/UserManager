namespace UserManager.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using UserManager.API.Models;
using UserManager.API.Models.DTOs;
using UserManager.API.Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        => Ok(new Response() { Data = await userService.RegisterAsync(request) });

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await userService.LoginAsync(request);
        if (result is null)
            return BadRequest(new Response()
            {
                Status = 403,
                Message = "Incorrect login or password",
            });
        return Ok(new Response { Data = result });
    }

    [HttpGet]
    public IActionResult GetAll()
        => Ok(new Response { Data = userService.GetAll() });

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await userService.DeleteAsync(id);
        if (result == 0)
            return BadRequest(new Response()
            {
                Status = 404,
                Message = "User is not found",
            });
        return Ok(new Response { Data = result });
    }

    [HttpPost("{id:long}/block")]
    public async Task<IActionResult> Block(long id)
    {
        var result = await userService.BlockAsync(id);
        if (result == 0)
            return BadRequest(new Response()
            {
                Status = 404,
                Message = "User is not found",
            });
        return Ok(new Response { Data = result });
    }

    [HttpPost("{id:long}/unblock")]
    public async Task<IActionResult> Unblock(long id)
    {
        var result = await userService.UnblockAsync(id);
        if (result == 0)
            return BadRequest(new Response()
            {
                Status = 404,
                Message = "User is not found",
            });
        return Ok(new Response { Data = result });
    }
}
