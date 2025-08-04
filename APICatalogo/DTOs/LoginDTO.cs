using System.ComponentModel.DataAnnotations;
using APICatalogo.Constants;

namespace APICatalogo.DTOs;

public class LoginDTO
{

    [Required(ErrorMessage=DataAnnotationMessages.REQUIRED)]
    public string? UserName { get; set; }

    [Required(ErrorMessage =DataAnnotationMessages.REQUIRED)]
    public string? Password { get; set; }
}
