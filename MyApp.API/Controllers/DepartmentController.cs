using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Services.Interface;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        private readonly IValidator<AddDepartmentDTO> validator;
        public DepartmentController(IDepartmentService departmentService, IValidator<AddDepartmentDTO> validator)
        {
            this.departmentService = departmentService;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await departmentService.GetDepartments();
            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDTO department)
        {
            var validationResult = await validator.ValidateAsync(department);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await departmentService.AddDepartment(department);
            if (!response.Success)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
