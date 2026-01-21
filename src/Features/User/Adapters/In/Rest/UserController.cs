using APIWEB.src.Features.User.Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace APIWEB.src.Features.User.Adapters.In.Rest
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;

        public UserController(ICreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var input = new ICreateUserUseCase.Input(request.Name, request.Email, request.Password);
                await _createUserUseCase.Execute(input);
                return Created(string.Empty, new { message = "User created successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}