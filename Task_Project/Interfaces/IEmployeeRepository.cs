using Task_Project.Models;

namespace Task_Project.Interfaces;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetEmployeeList();

    Employee Add(Employee employee);

    void AddEmployeeList(List<Employee> employees);

    Employee GetEmployee(string payrollId);

    // added delete method for later use in case needed...
    void Delete(Employee employee);

    void DeleteEmployeeList();

    void Update(Employee updatedEmployee);
}