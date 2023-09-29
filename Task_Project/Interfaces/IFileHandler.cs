namespace Task_Project.Interfaces;

using Models;

public interface IFileHandler
{
    string SaveFile(IFormFile file, IWebHostEnvironment hostEnvironment);
    
    List<Employee> ParseEmployeesFromCsv(string filePath);
}