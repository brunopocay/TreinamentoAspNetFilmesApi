using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTO
{
  public class UpdateCinemaDTO
  {
    [Required(ErrorMessage = "O Campo nome é obrigatório.")]
    public string Nome { get; set; }
  }
}
