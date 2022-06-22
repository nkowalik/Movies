using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoMapper;
using Movies.Api.Controllers;
using Movies.Api.DataCollectors;
using Movies.Api.Infrastructure.Entities;
using Movies.Api.Infrastructure.Repositories;
using Movies.Api.Models;
using NSubstitute;
using NUnit.Framework;

namespace Movies.Api.Tests.Controllers
{
    public class MoviesControllerTests
    {
        private IMoviesDataCollector _collector;
        private IFixture _fixture;
        private IMapper _mapper;

        private OmDbMoviesRepository _omdbRepo;
        private FakeDbMoviesRepository _fakedbRepo;
        private MoviesController _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
                ConfigureMembers = false
            });
            _collector = _fixture.Create<IMoviesDataCollector>();
            _mapper = _fixture.Create<IMapper>();
            _omdbRepo = _fixture.Create<OmDbMoviesRepository>();
            _fakedbRepo = _fixture.Create<FakeDbMoviesRepository>();

            _sut = new MoviesController(_omdbRepo, _fakedbRepo, _mapper);
        }

        [Test]
        public async Task WhenGetMoviesAsyncIsCalled_ThenProperResultIsReturned()
        {
            const string title = "movie title";

            var movies = new List<OmDbMovieEntity>()
            {
                new OmDbMovieEntity(title)
                {
                    Id = 1,
                    Year = "2020",
                    Director = "Director",
                    Genre = "Genre",
                    Plot = "Plot"
                }
            };
            _omdbRepo.GetMoviesAsync().Returns(movies);

            var result = await _sut.GetMoviesAsync("omdb");

            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                _mapper.Received(1).Map<IEnumerable<OmDbMovieDto>>(Arg.Any<IEnumerable<OmDbMovieEntity>>());
                await _omdbRepo.Received(1).GetMoviesAsync();
            });
        }
    }
}
