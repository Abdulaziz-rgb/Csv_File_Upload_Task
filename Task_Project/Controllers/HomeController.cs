namespace Task_Project.Controllers;

using Mappers;
using ViewModels;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Interface;


public class HomeController : Controller
{
    private static IEmployeeRepository? _employeeRepository;

    public HomeController(IEmployeeRepository employeeRepository)
    {
        // injecting dependency into controller
        _employeeRepository = employeeRepository;
    }
    public ViewResult Index()
    {
        var employees =  _employeeRepository.GetEmployeeList();
        
        return View("Index", employees);
    }
    

    [HttpPost]
    public IActionResult Index([FromForm] IFormFile file, [FromServices] IWebHostEnvironment hostEnvironment)
    {
        Console.Write(file.FileName);
        Console.Write(file.Name);
        Console.Write(file);
        // Handling file create and  upload process...
        
        string path = Path.Combine(hostEnvironment.WebRootPath, "files");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName = Path.GetFileName(file.FileName);
        string filePath = Path.Combine(path, fileName);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        using (FileStream fileStream = System.IO.File.Create(filePath))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        
        InsertEmployeeIntoDatabase(file.FileName);
        
        return RedirectToAction("Index", "Home");
    }

    
    [HttpGet]
    public ViewResult Edit(string payrollId)
    {
        Employee employee = _employeeRepository.GetEmployee(payrollId);
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
    
    private  void InsertEmployeeIntoDatabase(string fileName)
    {
        var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
        
        // Reading .csv file and parsing it into our model using mapper 

        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<CsvDataMapper>();
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var employee = csv.GetRecord<Employee>();
                
                // inserting data into database context
                
                _employeeRepository.Add(employee);
            }
        }
    }
}