using Task_Project.Models;

namespace Task_Project.Interfaces;

public interface IFileHandler
{
    string SaveFile(IFormFile file, IWebHostEnvironment hostEnvironment);
    
    List<Employee> ParseEmployeesFromCsv(string filePath);
}