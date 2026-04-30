using FluentValidation;
using HR.Application.Identity;
using HR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRApi.Controllers
{
    #region Auth Controller
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<LoginDto> _validator;

        public AuthController(IAuthService authService, IValidator<LoginDto> validator)
        {
            _authService = authService;
            _validator = validator;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));

            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    } 
    #endregion
}
