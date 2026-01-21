using APIWEB.src.Features.User.Domain.Ports;
using APIWEB.src.Features.User.Infrastructure.Dto;
using APIWEB.src.Features.User.Infrastructure.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace APIWEB.src.Features.User.Adapters.In.Rest
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IFindUserByIdUseCase _findUserByIdUseCase;

        public UserController(ICreateUserUseCase createUserUseCase, IFindUserByIdUseCase findUserByIdUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _findUserByIdUseCase = findUserByIdUseCase;
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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _findUserByIdUseCase.Execute(id);
                return Ok(UserMapper.ToDto(user));
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}