namespace Task_Project.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Key]
    [Display(Name = "Payroll ID")]
    public string PayrollNumber { get; set; }
    
    [Display(Name = "Firstname")]
    public string Forename { get; set; }
    
    [Display(Name = "Lastname")]
    public string Surname { get; set; }
    
    [Display(Name = "Date of birth")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }
    
    public int Telephone { get; set; }
    
    public int Mobile { get; set; }
    
    public string Address { get; set; }
    
    public string City { get; set; }
    
    public string Postcode { get; set; }
    
    [EmailAddress(ErrorMessage = "The email address is not valid")]
    public string Email { get; set; }
    
    [Display(Name = "Start date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
}