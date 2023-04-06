using F.Core.Controller;
using Microsoft.AspNetCore.Mvc;

namespace F.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : MainController
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register()
    {
        var data = new { data = 1 };
        return Ok(data);
    }
}
