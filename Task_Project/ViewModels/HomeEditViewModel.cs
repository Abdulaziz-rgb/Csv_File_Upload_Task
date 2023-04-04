namespace Task_Project.ViewModels;

using System.ComponentModel.DataAnnotations;

public class HomeEditViewModel
{
    [Display(Name = "Payroll ID")]
    public string PayrollNumber { get; set; }
    
    [Display(Name = "Firstname")]
    public string Forename { get; set; }
    
    [Display(Name = "Lastname")]
    public string Surname { get; set; }
    
    [Display(Name = "Date of birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }
    
    public int Telephone { get; set; }
    
    public int Mobile { get; set; }
    
    public string Address { get; set; }
    
    public string City { get; set; }

    public string Postcode { get; set; }
    
    [EmailAddress(ErrorMessage = "The email address is not valid")]
    public string Email { get; set; }
    
    [Display(Name = "Start date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }
}