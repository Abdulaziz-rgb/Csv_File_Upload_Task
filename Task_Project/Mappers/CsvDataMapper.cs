namespace Task_Project.Mappers;

using CsvHelper.Configuration;
using Models;


public sealed class CsvDataMapper : ClassMap<Employee>
{
    // Constructor for mapping csv data to model data respectively
        
    public CsvDataMapper()
    {
        const string format = "d/M/yyyy";
        Map(m => m.PayrollNumber).Index(0);
        Map(m => m.Forename).Index(1);
        Map(m => m.Surname).Index(2);
        Map(m => m.DateOfBirth).Index(3).TypeConverterOption.Format(format);
        Map(m => m.Telephone).Index(4);
        Map(m => m.Mobile).Index(5);
        Map(m => m.Address).Index(6);
        Map(m => m.City).Index(7);
        Map(m => m.Postcode).Index(8);
        Map(m => m.Email).Index(9);
        Map(m => m.StartDate).Index(10).TypeConverterOption.Format(format);
    }
}
