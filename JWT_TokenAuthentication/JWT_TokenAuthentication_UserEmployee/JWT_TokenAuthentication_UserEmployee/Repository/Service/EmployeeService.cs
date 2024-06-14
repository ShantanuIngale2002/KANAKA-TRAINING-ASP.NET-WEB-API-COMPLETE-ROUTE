using JWT_TokenAuthentication_UserEmployee.Data;
using JWT_TokenAuthentication_UserEmployee.Repository.EFCore;
using JWT_TokenAuthentication_UserEmployee.Repository.Interface;
using Microsoft.AspNetCore.Components.Forms;

namespace JWT_TokenAuthentication_UserEmployee.Repository.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtContext context;
        public EmployeeService(JwtContext context)
        {
            this.context = context;
        }





        public Employee AddEmployee(Employee employee)
        {
            var emp = context.Employees.Add(employee);

            context.SaveChanges();

            return emp.Entity;
        }



        public bool DeleteEmployee(int id)
        {
            var emp = context.Employees.SingleOrDefault(m => m.Id == id);

            if(emp == null)
            {
                throw new Exception("User not Found");
            }

            context.Employees.Remove(emp);

            int row = context.SaveChanges();

            if(row > 0) { return true; }

            return false;
        }



        public Employee GetEmployee(int id)
        {
            var emp = context.Employees.SingleOrDefault(m => m.Id == id);

            if (emp == null)
            {
                throw new Exception("User not Found");
            }

            Employee newEmp = new()
            {
                Name = emp.Name,
                Position = emp.Position,
                Company = emp.Company,
            };

            return newEmp;
        }



        public List<Employee> GetEmployeeDetails()
        {
            var emps = context.Employees.ToList();
            return emps;
        }



        public Employee UpdateEmployee(Employee Emp)
        {
            var emp = context.Employees.Update(Emp);
            context.SaveChanges();
            return emp.Entity;
        }
    }
}
