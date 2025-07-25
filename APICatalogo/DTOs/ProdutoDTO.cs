using APICatalogo.Constants;
using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ProdutoDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(200, ErrorMessage = DataAnnotationMessages.STRLEN)]
    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public string? ImagemUrl { get; set; }

    public int CategoriaId { get; set; }

}
