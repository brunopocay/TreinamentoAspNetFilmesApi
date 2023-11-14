using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTO
{
	public class ReadFilmeDTO
	{
		public string Titulo { get; set; } = string.Empty;
		public string GeneroFilme { get; set; } = string.Empty;
		public int IdadeMinima { get; set; }
		public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
        public ICollection<ReadSessaoDTO> Sessoes { get; set; }

    }
}
