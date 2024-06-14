using JWT_TokenAuthentication_UserEmployee.Data;
using JWT_TokenAuthentication_UserEmployee.Repository.Interface;
using JWT_TokenAuthentication_UserEmployee.Repository.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_TokenAuthentication_UserEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // now this controller methods are only accessible with token generated after user login.
    public class EmployeeController : ControllerBase
    {

        private EmployeeService employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
    




        // GET: api/<EmployeeController>
        [HttpGet]
        public List<Employee> Get()
        {
            var Employees = employeeService.GetEmployeeDetails();
            return Employees;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            var Employee = employeeService.GetEmployee(id);
            return Employee;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public Employee Post([FromBody] Employee employee)
        {
            var Emp = employeeService.AddEmployee(employee);
            return Emp;
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public Employee Put(int id, [FromBody] Employee value)
        {
            var Emp = employeeService.UpdateEmployee(value);
            return Emp;
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            bool isDone = employeeService.DeleteEmployee(id);
            return isDone;
        }
    }
}
