using CashManager.Application.UseCases.Users.Register;
using CashManager.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterUser(
        [FromServices] IRegisterUserUseCase userUseCase,
        [FromBody] RequestRegisterUser request)
    {
        await userUseCase.Execute(request);

        return StatusCode(StatusCodes.Status201Created);
    }
}
