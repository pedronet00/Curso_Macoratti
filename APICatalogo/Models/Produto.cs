using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using APICatalogo.Constants;
using APICatalogo.Validations;

namespace APICatalogo.Models;

public class Produto
{

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required(ErrorMessage = DataAnnotationMessages.REQUIRED)]
    [StringLength(200, ErrorMessage = DataAnnotationMessages.STRLEN)]
    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

}
