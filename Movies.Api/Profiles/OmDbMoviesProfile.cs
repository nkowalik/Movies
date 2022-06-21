using AutoMapper;
using Movies.Api.Infrastructure.Entities;
using Movies.Api.Models;

namespace Movies.Api.Profiles
{
    public class OmDbMoviesProfile : Profile
    {
        public OmDbMoviesProfile()
        {
            CreateMap<OmDbMovieEntity, OmDbMovieDto>();
            CreateMap<OmDbMovieDto, OmDbMovieEntity>();
        }
    }
}
