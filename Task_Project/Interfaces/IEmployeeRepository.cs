namespace Task_Project.Interfaces;

using Models;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetEmployees();

    Employee Add(Employee employee);

    void AddEmployees(List<Employee> employees);

    Employee GetEmployee(string payrollId);

    // added delete method for later use in case needed...
    void Delete(Employee employee);

    void DeleteEmployeeList(List<Employee> employeesToDelete);

    void Update(Employee updatedEmployee);
}