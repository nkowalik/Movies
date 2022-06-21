using AutoMapper;
using Movies.Api.Infrastructure.Entities;
using Movies.Api.Models;

namespace Movies.Api.Profiles
{
    public class FakeDbMovieDetailsProfile : Profile
    {
        public FakeDbMovieDetailsProfile()
        {
            CreateMap<FakeDbMovieDetailsEntity, FakeDbMovieDetailsDto>();
            CreateMap<FakeDbMovieDetailsDto, FakeDbMovieDetailsEntity>();
        }
    }
}
