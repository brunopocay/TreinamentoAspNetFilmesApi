using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
	public class Filme
	{
		[Key]
		[Required]
		public int Id { get; set; }
		public string Titulo { get; set; } = string.Empty;
		public string GeneroFilme { get; set; } = string.Empty;
		public int IdadeMinima { get; set; }
        public virtual ICollection<Sessao> Sessoes  { get; set; }
    }
}
