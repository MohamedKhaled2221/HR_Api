using FluentValidation;
using HR.Application.Attendance;
using HR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;
        private readonly IValidator<CreateAttendanceDto> _createValidator;
        private readonly IValidator<UpdateAttendanceDto> _updateValidator;
        private readonly IValidator<AttendanceFilterDto> _filterValidator;

        public AttendanceController(
            IAttendanceService service,
            IValidator<CreateAttendanceDto> createValidator,
            IValidator<UpdateAttendanceDto> updateValidator,
            IValidator<AttendanceFilterDto> filterValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _filterValidator = filterValidator;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AttendanceFilterDto filter)
        {
            var validation = await _filterValidator.ValidateAsync(filter);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));

            var attendances = await _service.GetAllAsync(filter);
            return Ok(attendances);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attendance = await _service.GetByIdAsync(id);
            if (attendance == null)
                return NotFound(new { message = "سجل الحضور غير موجود" });

            return Ok(attendance);
        }

     
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAttendanceDto dto)
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

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAttendanceDto dto)
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
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { message = "تم حذف سجل الحضور بنجاح" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

       
        [HttpPost("import")]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "من فضلك ارفع ملف Excel صحيح" });

            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BadRequest(new { message = "صيغة الملف غير مدعومة — يُقبل xlsx أو xls فقط" });

            var result = await _service.ImportFromExcelAsync(file);
            return Ok(result);
        }
    }
}
