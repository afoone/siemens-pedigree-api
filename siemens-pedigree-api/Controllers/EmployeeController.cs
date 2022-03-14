using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using siemens_pedigree_api.Models;
using Microsoft.EntityFrameworkCore;

namespace siemens_pedigree_api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeContext _context;

        private bool EmployeeExists(long id)
        {
            return _context.employees.Any(e => e.Id == id);
        }

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public  IEnumerable<Employee> Get()
        {
            Console.WriteLine("en el sitio");
            IEnumerable<Employee> employees = _context.employees;
            return employees;
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Employee>> Get(long id)
        {
            Employee employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return  employee;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult> Post(Employee employee)
        {
            Console.WriteLine(employee);
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = employee.Id, employee });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            } catch
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return employee;
        }



        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
