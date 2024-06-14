using JWT_RoleBasedAuthorization_WebApiApp.Data;

namespace JWT_RoleBasedAuthorization_WebApiApp.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetEmployeeDetails();
        public Employee AddEmployee(Employee employee);
    }
}
