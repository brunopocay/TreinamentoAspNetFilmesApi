using AutoMapper;
using FilmesApi.DTO;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
  public class CinemaProfile : Profile
  {
    public CinemaProfile()
    {
      CreateMap<CreateCinemaDTO, Cinema>();
      CreateMap<Cinema, ReadCinemaDTO>()
                .ForMember(cinemadto => cinemadto.Endereco, 
                    opt => opt.MapFrom(cinema => cinema.Endereco))
				.ForMember(cinemadto => cinemadto.Sessoes,
					opt => opt.MapFrom(cinema => cinema.Sessoes));
			CreateMap<UpdateCinemaDTO, Cinema>();
    }
  }
}