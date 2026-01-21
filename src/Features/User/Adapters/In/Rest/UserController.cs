using APIWEB.src.Features.User.Domain.Ports;
using APIWEB.src.Features.User.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _findUserByIdUseCase.Execute(id);
                var userDto = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}