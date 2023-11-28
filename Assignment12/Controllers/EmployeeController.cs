using Assignment12.Data;
using Assignment12.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeAPIDbContext _empDbContext;
        public EmployeeController(EmployeeAPIDbContext empDbContext)
        {
            _empDbContext = empDbContext; 
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
           return Ok(_empDbContext.Employee.ToList()); 
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployee addEmployee)
        {
            var emplyee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = addEmployee.FirstName,
                LastName = addEmployee.LastName,
                Email = addEmployee.Email,
            };
            await _empDbContext.Employee.AddAsync(emplyee);
            await _empDbContext.SaveChangesAsync();
            return Ok(emplyee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, UpdateEmployee updateContact)
        {
            var employee = await _empDbContext.Employee.FindAsync(id);

            if(employee != null)
            {
                employee.FirstName = updateContact.FirstName;
                employee.LastName = updateContact.LastName; 
                employee.Email = updateContact.Email;
                await _empDbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = _empDbContext.Employee.Find(id);
            if(employee != null)
            {
                _empDbContext.Employee.Remove(employee);
                await _empDbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();
        }
    }
}
