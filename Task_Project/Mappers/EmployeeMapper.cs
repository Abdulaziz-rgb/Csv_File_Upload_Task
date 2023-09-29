namespace Task_Project.Mappers;

using ViewModels;
using Models;

public static class EmployeeMapper
{
    public static Employee MapUpdatedEmployee(Employee existingEmployee, Employee updatedEmployee)
    {
        existingEmployee.Forename = updatedEmployee.Forename;
        existingEmployee.Surname = updatedEmployee.Surname;
        existingEmployee.DateOfBirth = updatedEmployee.DateOfBirth;
        existingEmployee.StartDate = updatedEmployee.StartDate;
        existingEmployee.Telephone = updatedEmployee.Telephone;
        existingEmployee.Mobile = updatedEmployee.Mobile;
        existingEmployee.Address = updatedEmployee.Address;
        existingEmployee.City = updatedEmployee.City;
        existingEmployee.Postcode = updatedEmployee.Postcode;
        existingEmployee.Email = updatedEmployee.Email;

        return existingEmployee;
    }

    public static HomeEditViewModel MapEmployeeToViewModel(Employee employee)
    {
        return new HomeEditViewModel()
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
    }
}