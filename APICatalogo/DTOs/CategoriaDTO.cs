using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class CategoriaDTO
{

    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    public string? ImagemUrl { get; set; }
}
