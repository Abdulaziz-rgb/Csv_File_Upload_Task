namespace Task_Project.Repository;

using Interfaces;
using DataAccess;
using Models;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddEmployees(List<Employee> employees)
    {
       _dbContext.AddRange(employees);
       _dbContext.SaveChanges();
    }

    public Employee GetEmployee(string payrollNumber)
    {
        return _dbContext.Employees.Find(payrollNumber);
    }

    public IEnumerable<Employee>? GetEmployees()
    {
        return _dbContext.Employees;
    }

    public Employee Add(Employee employee)
    {
        _dbContext.Employees?.Add(employee);
        _dbContext.SaveChanges();
        return employee;
    }

    public void Delete(Employee employee)
    {
        _dbContext.Remove(employee);
        _dbContext.SaveChanges();
    }

    public void DeleteEmployeeList(List<Employee> employeesToDelete)
    {
        _dbContext.RemoveRange(employeesToDelete);
        _dbContext.SaveChanges();
    }
        
    public void Update(Employee updatedEmployee)
    {
        var staff = _dbContext.Employees.Attach(updatedEmployee);
        staff.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _dbContext.SaveChanges();
    }
}