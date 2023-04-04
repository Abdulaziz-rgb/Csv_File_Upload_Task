using Task_Project.Models;

namespace Task_Project.Interface
{
    public interface IEmployeeRepository
    {

        IEnumerable<Employee> GetEmployeeList();

        Employee Add(Employee employee);

        Employee GetEmployee(string payrollId);

        void Delete(Employee employee);

        void DeleteEmployeeList();

        void Update(Employee updatedEmployee);
    }
}
