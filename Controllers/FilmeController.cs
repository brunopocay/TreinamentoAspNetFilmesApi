using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTO;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FilmesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilmeController : ControllerBase
	{
		private readonly FilmesContext _context;
		private readonly IMapper _mapper;

		public FilmeController(FilmesContext context, IMapper mapper)
		{
			_mapper = mapper;
			_context = context;
		}

		/// <summary>
		/// Adiciona um filme ao banco de dados
		/// </summary>
		/// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
		/// <returns>IActionResult</returns>
		/// <response code="201">Caso inserção seja feita com sucesso</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public IActionResult AdicionaFilme([FromBody] FilmesDTO filmeDto)
		{
			//Fazer um map *PARA* um filme a *PARTIR* de um DTO;
			Filme filme = _mapper.Map<Filme>(filmeDto);
			_context.Filmes.Add(filme);
			_context.SaveChanges();
			return CreatedAtAction(nameof(FilmeById), new { id = filme.Id }, filme);
		}

		[HttpGet]
		public IEnumerable<ReadFilmeDTO> GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? nomeCinema = null)
		{
			if(nomeCinema is null)
				return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take).ToList());

			return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take).Where(filme => filme.Sessoes
				.Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
		}

		[HttpGet("{Id}")]
		public IActionResult FilmeById(int Id)
		{
			var filme = _context.Filmes.FirstOrDefault(filmeid => filmeid.Id == Id);

			if (filme is null)
				return NotFound();

			var filmeDto = _mapper.Map<ReadFilmeDTO>(filme);

			return Ok(filmeDto);
		}

		[HttpPut("Atualizar/{id}")]
		public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
		{
			var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

			if (filme == null)
				return NotFound();

			_mapper.Map(filmeDTO, filme);
			_context.SaveChanges();

			return NoContent();
		}

		[HttpPatch("AtualizarParcial/{id}")]
		public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
		{
			var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

			if (filme == null)
				return NotFound();

			var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);
			patch.ApplyTo(filmeParaAtualizar, ModelState);

			if (!TryValidateModel(filmeParaAtualizar))
				return ValidationProblem(ModelState);

			_mapper.Map(filmeParaAtualizar, filme);
			_context.SaveChanges();

			return NoContent();
		}

		[HttpDelete("DeletarFilme/{id}")]
		public IActionResult DeletarFilme(int id)
		{
			var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

			if (filme == null)
				return NotFound();

			_context.Remove(filme);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
