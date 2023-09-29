namespace Task_Project.Handlers;

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Interfaces;
using Mappers;
using Models;

public class FileHandler : IFileHandler
{
    public string SaveFile(IFormFile file, IWebHostEnvironment hostEnvironment)
    {
        var path = Path.Combine(hostEnvironment.WebRootPath, "files");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var fileName = Path.GetFileName(file.FileName);
        var filePath = Path.Combine(path, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        using (var fileStream = File.Create(filePath))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        return filePath;
    }

    public List<Employee> ParseEmployeesFromCsv(string filePath)
    {
        // Reading .csv file and parsing it into our model using mapper 
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<CsvDataMapper>();
            return csv.GetRecords<Employee>().ToList();
        }
    }
}