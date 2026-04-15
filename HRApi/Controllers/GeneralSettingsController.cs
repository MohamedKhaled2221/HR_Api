using FluentValidation;
using HR.Application.GeneralSettings.Dtos;
using HR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    #region GeneralSettings Controller
    public class GeneralSettingsController : ControllerBase
    {
        private readonly IGeneralSettingsService _service;
        private readonly IValidator<UpdateGeneralSettingsDto> _validator;

        public GeneralSettingsController(
            IGeneralSettingsService service,
            IValidator<UpdateGeneralSettingsDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var settings = await _service.GetAsync();
            if (settings == null)
                return NotFound(new { message = "لم يتم العثور على الإعدادات" });

            return Ok(settings);
        }

        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGeneralSettingsDto dto)
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
                var updated = await _service.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        } 
        #endregion
    }
}
