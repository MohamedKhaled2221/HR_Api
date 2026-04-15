using FluentValidation;
using HR.Application.OfficialHoliday.Dtos;
using HR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    #region OfficialHolidays Controller
    public class OfficialHolidaysController : ControllerBase
    {
        private readonly IOfficialHolidayService _service;
        private readonly IValidator<CreateOfficialHolidayDto> _createValidator;
        private readonly IValidator<UpdateOfficialHolidayDto> _updateValidator;

        public OfficialHolidaysController(
            IOfficialHolidayService service,
            IValidator<CreateOfficialHolidayDto> createValidator,
            IValidator<UpdateOfficialHolidayDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // ── GET /api/officialholidays?year=2024 ───────────
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? year = null)
        {
            var holidays = await _service.GetAllAsync(year);
            return Ok(holidays);
        }

        // ── GET /api/officialholidays/{id} ────────────────
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var holiday = await _service.GetByIdAsync(id);
            if (holiday == null)
                return NotFound(new { message = "الإجازة غير موجودة" });

            return Ok(holiday);
        }

        // ── POST /api/officialholidays ────────────────────
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOfficialHolidayDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // ── PUT /api/officialholidays/{id} ────────────────
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOfficialHolidayDto dto)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // ── DELETE /api/officialholidays/{id} ─────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { message = "تم حذف الإجازة بنجاح" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        } 
        #endregion
    }
}

