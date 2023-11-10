using AutoMapper;
using FilmesApi.DTO;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
  public class FilmesMapper : Profile
  {
    public FilmesMapper()
    {
      CreateMap<FilmesDTO, Filme>();
      CreateMap<UpdateFilmeDTO, Filme>();
      CreateMap<Filme, UpdateFilmeDTO>();
      CreateMap<Filme, ReadFilmeDTO>();
    }
  }
}
