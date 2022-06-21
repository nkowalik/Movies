using AutoMapper;
using Movies.Api.Infrastructure.Entities;
using Movies.Api.Models;

namespace Movies.Api.Profiles
{
    public class FakeDbMoviesProfile : Profile
    {
        public FakeDbMoviesProfile()
        {
            CreateMap<FakeDbMovieEntity, FakeDbMovieDto>();
            CreateMap<FakeDbMovieDto, FakeDbMovieEntity>();
        }
    }
}
