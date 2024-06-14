using JWT_RoleBasedAuthorization_WebApiApp.Data;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JWT_RoleBasedAuthorization_WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // now this controller methods are only accessible with token generated after user login. Common for every request or role.
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository employeeRepoServe;
        public EmployeeController(IEmployeeRepository employeeRepoServe)
        {
            this.employeeRepoServe = employeeRepoServe;
        }


        // we can apply authorize like this to methods for customs authroization requiements where other methods will get open
        [Authorize(Roles = "User")]  // any request made with Roles User and Admin generating token can access this method
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            return employeeRepoServe.GetEmployeeDetails();
        }

        [Authorize(Roles = "User")] // only requests made by admin can only access this method ofc using generated token
        [HttpPost]
        public Employee AddEmployee([FromBody] Employee emp)
        {
            var addedEmployee = employeeRepoServe.AddEmployee(emp);
            return addedEmployee;
        }
    }
}
