using Azure.Core;
using JWT_RoleBasedAuthorization_WebApiApp.Data;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.EFCore;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.Interface;

namespace JWT_RoleBasedAuthorization_WebApiApp.Repository.Service
{
    public class EmployeeService : IEmployeeRepository
    {

        private readonly RoleBasedAppContext context;
        public EmployeeService(RoleBasedAppContext context)
        {
            this.context = context;
        }



        public Employee AddEmployee(Employee employee)
        {
            var emp = context.Employees.Add(employee);
            context.SaveChanges();
            return emp.Entity;
        }

        public List<Employee> GetEmployeeDetails()
        {
            var emps = context.Employees.ToList();
            return emps;
        }
    }
}
