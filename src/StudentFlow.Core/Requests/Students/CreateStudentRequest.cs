using System.ComponentModel.DataAnnotations;
using StudentFlow.Core.Models;

namespace StudentFlow.Core.Requests.Students;

public class CreateStudentRequest : BaseRequest
{
    [Required(ErrorMessage = "Nome Inválido")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Matrícula Inválida")]
    public string Registration { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email Inválido")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Curso Inválido")]
    public Course Course { get; set; } = new();

    [Required(ErrorMessage = "Aniversário Inválido")]
    public DateTime DateOfBirth { get; set; }
}