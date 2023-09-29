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

    public void AddEmployeeList(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            _dbContext.Add(employee);
            _dbContext.SaveChanges();
        }
    }

    public Employee GetEmployee(string payrollNumber)
    {
        return _dbContext.Employees.Find(payrollNumber);
    }

    public IEnumerable<Employee>? GetEmployeeList()
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

    public void DeleteEmployeeList()
    {
        foreach (var employee in _dbContext.Employees)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }
    }
        
    public void Update(Employee updatedEmployee)
    {
        var staff = _dbContext.Employees.Attach(updatedEmployee);
        staff.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _dbContext.SaveChanges();
    }
}