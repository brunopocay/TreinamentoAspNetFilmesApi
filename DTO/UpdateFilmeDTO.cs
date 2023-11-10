using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTO
{
  public class UpdateFilmeDTO
  {
    
    [Required(ErrorMessage = "Titulo é obrigatório")]
    [StringLength(100)]
    public string Titulo { get; set; } = string.Empty;
    [Required(ErrorMessage = "Genero é obrigatório")]
    [StringLength(40)]
    public string GeneroFilme { get; set; } = string.Empty;

    [Required(ErrorMessage = "Idade Minima é obrigatório")]
    public int IdadeMinima { get; set; }
    
  }
}
