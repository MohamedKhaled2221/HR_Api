using FluentValidation;
using HR.Application.SalaryReport.Dtos;
using HR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _service;
        private readonly IValidator<SalaryFilterDto> _validator;

        public SalaryController(
            ISalaryService service,
            IValidator<SalaryFilterDto> validator)
        {
            _service = service;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SalaryFilterDto filter)
        {
            var validation = await _validator.ValidateAsync(filter);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));

            var report = await _service.GetAllAsync(filter);
            return Ok(report);
        }
        [HttpGet("employee")]
        public async Task<IActionResult> GetByEmployee([FromQuery] SalaryFilterDto filter)
        {
            var validation = await _validator.ValidateAsync(filter);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));

            if (!filter.EmployeeId.HasValue)
                return BadRequest(new { message = "من فضلك ادخل اسم موظف صالح" });

            var report = await _service.GetByEmployeeAsync(filter);
            if (report == null)
                return NotFound(new { message = "من فضلك ادخل اسم موظف صالح" });

            return Ok(report);
        }
    }
}