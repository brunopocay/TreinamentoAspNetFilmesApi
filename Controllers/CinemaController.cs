using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTO;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CinemaController : ControllerBase
	{
		private readonly FilmesContext _context;
		private readonly IMapper _mapper;

		public CinemaController(FilmesContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}


		[HttpPost]
		public IActionResult AdicionaCinema([FromBody] CreateCinemaDTO cinemaDto)
		{
			Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
			_context.Cinemas.Add(cinema);
			_context.SaveChanges();
			return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinemaDto);
		}

		[HttpGet]
		public IEnumerable<ReadCinemaDTO> RecuperaCinemas([FromQuery]int? enderecoId = null)
		{
			if(enderecoId is null)
				return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.ToList());

			return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas WHERE Cinemas.EnderecoId = {enderecoId}").ToList());
		}

		[HttpGet("{id}")]
		public IActionResult RecuperaCinemasPorId(int id)
		{
			Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
			if (cinema != null)
			{
				ReadCinemaDTO cinemaDto = _mapper.Map<ReadCinemaDTO>(cinema);
				return Ok(cinemaDto);
			}
			return NotFound();
		}

		[HttpPut("{id}")]
		public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDTO cinemaDto)
		{
			Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
			if (cinema == null)
			{
				return NotFound();
			}
			_mapper.Map(cinemaDto, cinema);
			_context.SaveChanges();
			return NoContent();
		}


		[HttpDelete("{id}")]
		public IActionResult DeletaCinema(int id)
		{
			Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
			if (cinema == null)
			{
				return NotFound();
			}
			_context.Remove(cinema);
			_context.SaveChanges();
			return NoContent();
		}

	}
}


