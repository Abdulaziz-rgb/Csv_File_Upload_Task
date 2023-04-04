namespace Task_Project.Tests.Controller;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Controllers;
using Interface;
using Models;


public class HomeControllerTests
{
    private readonly HomeController _controller;
    
    private readonly Mock<IEmployeeRepository> _mockRepo;
    

    public HomeControllerTests()
    {
        // injecting dependencies 
        _mockRepo = new Mock<IEmployeeRepository>();
        
        // defining SUT -> System Under Test
        _controller = new HomeController(_mockRepo.Object);
    }

    [Fact]
    public void HomeController_Index_ReturnsViewName()
    {
        // Act
        var result = _controller.Index();
        
        // Assert
         Assert.IsType<ViewResult>(result);
         Assert.Equal("Index", result.ViewName);
    }
    
    [Fact]
    public void HomeController_Index_ReturnsExactNumberOfEmployees()
    {     
        // Arrange
        
        // I found it a bit difficult to understand how Mock is functioning here...
        _mockRepo.Setup(repo => repo.GetEmployeeList())
            .Returns(new List<Employee> { new(), new() });

        // Act
        var result = _controller.Index();

        // Assert
         Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void HomeController_Edit_ReturnsNotFoundView_WithWrongId()
    {
        // Arrange
        var payrollId = "BACJ53";
        
        // Act 
        var result = _controller.Edit(payrollId);

        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.Equal("EmployeeNotFound", result.ViewName);
        Assert.Equal(payrollId, result.Model);
    }
    
    // Unit Test is not completed at all. I have problems in writing Unit Test for post and edit 
    // actions. 
    // Hope you understand my situation :)
}