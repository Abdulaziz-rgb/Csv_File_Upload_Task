namespace Task_Project.Tests.Controller;

using NUnit.Framework;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Controllers;
using Models;
using FluentAssertions;
using ViewModels;

[TestFixture]
public class HomeControllerTests
{
    private readonly HomeController _controller;
    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly Mock<IFileHandler> _mockFileHandler;
    
    public HomeControllerTests()
    {
        // injecting dependencies 
        _mockRepository = new Mock<IEmployeeRepository>();
        _mockFileHandler = new Mock<IFileHandler>();
        
        // Initializing SUT -> System Under Test
         _controller = new HomeController(_mockRepository.Object, _mockFileHandler.Object);
    }

    [Test]
    public void HomeController_Index_ReturnsView()
    {
        // Act
        var result = _controller.Index();
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.ViewName.Should().Be("Index");
    }
    
    [Test]
    public void HomeController_Index_ReturnsExactNumberOfEmployees()
    {     
        // Arrange
        _mockRepository.Setup(repo => repo.GetEmployees())
            .Returns(new List<Employee> { new(), new() });

        // Act
        var result = _controller.Index();

        // Assert
        result.Should().BeOfType<ViewResult>();
        result.Model.Should().BeOfType<List<Employee>>();
    }

    [Test]
    public void HomeController_Edit_ReturnsViewWithEmployee()
    {
        // Arrange
        var expectedEmployee = new Employee()
        {
            PayrollNumber = "COOP08",
            Forename = "John",
            Surname = "William",
            DateOfBirth = new DateTime(1955, 01, 26),
            Telephone = 12345678,
            Mobile = 987654231,
            Address = "12 Foreman road",
            City = "London",
            Postcode = "GU12 6JW",
            Email = "nomadic20@hotmail.co.uk",
            StartDate = new DateTime(2013, 04, 18)
        };
        _mockRepository.Setup(repo => repo.GetEmployee(It.IsAny<string>())).Returns(expectedEmployee);
        
        // Act
        var result = _controller.Edit("COOP08");
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.Model.Should().BeOfType<HomeEditViewModel>();
    }

    [Test]
    [TestCase("BACJ53")]
    public void HomeController_Edit_ReturnsNotFoundView_WithWrongId(string payrollId)
    {
        // Act 
        var result = _controller.Edit(payrollId);

        // Assert
        result.Should().BeOfType<ViewResult>();
        result.ViewName.Should().Be("EmployeeNotFound");
    }
}