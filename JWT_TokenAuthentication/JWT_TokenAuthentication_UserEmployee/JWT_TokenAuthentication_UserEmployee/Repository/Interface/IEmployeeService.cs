using JWT_TokenAuthentication_UserEmployee.Data;

namespace JWT_TokenAuthentication_UserEmployee.Repository.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployeeDetails();
        public Employee GetEmployee(int id);
        public Employee AddEmployee(Employee employee);
        public Employee UpdateEmployee(Employee employee);
        public bool DeleteEmployee(int id);
    }
}
