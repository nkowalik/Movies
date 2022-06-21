using AutoMapper;
using Movies.Api.Entities;
using Movies.Api.Models;

namespace Movies.Api.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<MovieEntity, MovieDto>();
            CreateMap<MovieDto, MovieEntity>();
        }
    }
}
