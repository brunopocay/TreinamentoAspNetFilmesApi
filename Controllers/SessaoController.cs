using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTO;
using FilmesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessaoController : ControllerBase
	{

		private readonly FilmesContext _context;
		private readonly IMapper _mapper;

		public SessaoController(FilmesContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpPost]
		public IActionResult AdicionaSessao(CreateSessaoDTO dto)
		{
			Sessao sessao = _mapper.Map<Sessao>(dto);
			_context.Sessoes.Add(sessao);
			_context.SaveChanges();
			return CreatedAtAction(nameof(RecuperaSessoesPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
		}

		[HttpGet]
		public IEnumerable<ReadSessaoDTO> RecuperaSessoes()
		{
			return _mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.ToList());
		}

		[HttpGet("{filmeId}/{cinemaId}")]
		public IActionResult RecuperaSessoesPorId(int filmeId, int cinemaId)
		{
			Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
			if (sessao != null)
			{
				ReadSessaoDTO sessaoDto = _mapper.Map<ReadSessaoDTO>(sessao);

				return Ok(sessaoDto);
			}
			return NotFound();
		}
	}
}

