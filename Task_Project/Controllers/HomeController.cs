namespace Task_Project.Controllers;

using Mappers;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

public class HomeController : Controller
{
    private IEmployeeRepository? _employeeRepository;
    
    private readonly IFileHandler _fileHandler;

    public HomeController(IEmployeeRepository employeeRepository, IFileHandler fileHandler)
    {
        // injecting dependency into controller
        _employeeRepository = employeeRepository;
        _fileHandler = fileHandler;
    }
    public ViewResult Index()
    {
        var employees =  _employeeRepository.GetEmployees();
        return View("Index", employees);
    }

    [HttpPost]
    public IActionResult Index([FromForm] IFormFile file, [FromServices] IWebHostEnvironment hostEnvironment)
    {
        var availableEmployees = _employeeRepository.GetEmployees().ToList();
        if (availableEmployees != null)
        {
            _employeeRepository.DeleteEmployeeList(availableEmployees);
        }
        var filePath = _fileHandler.SaveFile(file, hostEnvironment);
        InsertEmployeeIntoDatabase(filePath);
        
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public ViewResult Edit(string payrollId)
    {
        var employee = _employeeRepository.GetEmployee(payrollId);
        if (employee == null)
        {
            return View("EmployeeNotFound", payrollId);
        }
        var editViewModel = EmployeeMapper.MapEmployeeToViewModel(employee);
        
        return View(editViewModel);
    }
    
    [HttpPost]
    public IActionResult Edit(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var existingEmployee = _employeeRepository.GetEmployee(employee.PayrollNumber);
        var updatedEmployee = EmployeeMapper.MapUpdatedEmployee(existingEmployee, employee);
        _employeeRepository.Update(updatedEmployee);
        
        return RedirectToAction("Index");
    }
    
    public IActionResult DeleteAll()
    {
        var employees = _employeeRepository.GetEmployees().ToList();
        if (employees != null)
        {
            _employeeRepository.DeleteEmployeeList(employees);
        }

        return View("Index", _employeeRepository.GetEmployees());
    }
        
    private void InsertEmployeeIntoDatabase(string fileName)
    {
        var employees = _fileHandler.ParseEmployeesFromCsv(fileName);
        _employeeRepository.AddEmployees(employees);
    }
}