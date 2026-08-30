using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Services.Interface;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService employeeService;
        private readonly IValidator<AddEmployeeDTO> validator;
        public EmployeeController(IEmployeeService employeeService, IValidator<AddEmployeeDTO> validator)
        {
            this.employeeService = employeeService;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employees = await employeeService.GetEmployee();
            return Ok(employees);
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetEmployeeByName(string name)
        {
            var employee = await employeeService.GetEmployeeByName(name);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDTO employee)
        {
            var validationResult = await validator.ValidateAsync(employee);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var newEmployee = await employeeService.AddEmployee(employee);
            return Ok(newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, AddEmployeeDTO employee)
        {
            var updatedEmployee = await employeeService.UpdateEmployee(employee, id);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deletedEmployee = await employeeService.DeleteEmployee(id);
            return Ok(deletedEmployee);
        }

        [HttpDelete("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {
            var deletedEmployees = await employeeService.BulkDelete(ids);
            return Ok(deletedEmployees);
        }
    }
}
