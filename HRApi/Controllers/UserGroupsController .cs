using FluentValidation;
using HR.Application.Identity;
using HR.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserGroupsController : ControllerBase
    {
        private readonly IUserGroupService _service;
        private readonly IValidator<CreateUserGroupDto> _createValidator;
        private readonly IValidator<UpdateUserGroupDto> _updateValidator;

        public UserGroupsController(
            IUserGroupService service,
            IValidator<CreateUserGroupDto> createValidator,
            IValidator<UpdateUserGroupDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _service.GetAllAsync();
            return Ok(groups);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _service.GetByIdAsync(id);
            if (group == null)
                return NotFound(new { message = "المجموعة غير موجودة" });
            return Ok(group);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserGroupDto dto)
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserGroupDto dto)
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
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { message = "تم حذف المجموعة بنجاح" });
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
