using CsvHelper.Configuration;
using Task_Project.Interfaces;

namespace Task_Project.Controllers;

using Mappers;
using ViewModels;
using System.Globalization;
using CsvHelper;
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
        var employees =  _employeeRepository.GetEmployeeList();
        return View("Index", employees);
    }
    

    [HttpPost]
    public IActionResult Index([FromForm] IFormFile file, [FromServices] IWebHostEnvironment hostEnvironment)
    {
        if (_employeeRepository.GetEmployeeList() != null)
        {
            _employeeRepository.DeleteEmployeeList();
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
            return View("EmployeeNotFound", payrollId);;
        }

        var editViewModel = new HomeEditViewModel()
        {
            PayrollNumber = employee.PayrollNumber,
            Forename = employee.Forename,
            Surname = employee.Surname,
            DateOfBirth = employee.DateOfBirth.Date,
            Telephone = employee.Telephone,
            Mobile = employee.Mobile,
            Address = employee.Address,
            City = employee.City,
            Postcode = employee.Postcode,
            Email = employee.Email,
            StartDate = employee.StartDate.Date
            };
        
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
        
        existingEmployee.Forename = employee.Forename;
        existingEmployee.Surname = employee.Surname;
        existingEmployee.DateOfBirth = employee.DateOfBirth;
        existingEmployee.StartDate = employee.StartDate;
        existingEmployee.Telephone = employee.Telephone;
        existingEmployee.Mobile = employee.Mobile;
        existingEmployee.Address = employee.Address;
        existingEmployee.City = employee.City;
        existingEmployee.Postcode = employee.Postcode;
        existingEmployee.Email = employee.Email;
        
        _employeeRepository.Update(employee);
        
        return RedirectToAction("Index");
    }
    
    public IActionResult DeleteAll()
    {
        var employees = _employeeRepository.GetEmployeeList();

        if (employees != null)
        {
            _employeeRepository.DeleteEmployeeList();
        }

        return View("Index", _employeeRepository.GetEmployeeList());
    }
        
    private void InsertEmployeeIntoDatabase(string fileName)
    {
        var employees = _fileHandler.ParseEmployeesFromCsv(fileName);
        _employeeRepository.AddEmployeeList(employees);
    }
}