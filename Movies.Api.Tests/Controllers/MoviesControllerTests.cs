using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoMapper;
using Movies.Api.Controllers;
using Movies.Api.Infrastructure.Entities;
using Movies.Api.Infrastructure.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace Movies.Api.Tests.Controllers
{
    public class MoviesControllerTests
    {
        private IFixture _fixture;
        private IMapper _mapper;
        private IMoviesRepository<OmDbMovieEntity> _omdbRepo;
        private IMoviesRepository<FakeDbMovieEntity> _fakedbRepo;

        private MoviesController _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
                ConfigureMembers = false
            });
            _mapper = _fixture.Create<IMapper>();
            _omdbRepo = _fixture.Create<IMoviesRepository<OmDbMovieEntity>>();
            _fakedbRepo = _fixture.Create<IMoviesRepository<FakeDbMovieEntity>>();

            _sut = new MoviesController(_omdbRepo, _fakedbRepo, _mapper);
        }

        [Test]
        public async Task WhenGetMoviesAsyncWithOmDbIsCalled_ThenProperResultIsReturned()
        {
            const string title = "movie title";
            const string dbName = "omdb";

            var movies = new List<OmDbMovieEntity>()
            {
                CreateTestOmDbMovie(title)
            };
            _omdbRepo.GetMoviesAsync().Returns(movies);

            var result = await _sut.GetMoviesAsync(dbName);

            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                await _omdbRepo.Received(1).GetMoviesAsync();
            });
        }

        [Test]
        public async Task WhenGetMoviesAsyncWithFakeDbIsCalled_ThenProperResultIsReturned()
        {
            const string title = "movie title";
            const string dbName = "fakedb";

            var movies = new List<FakeDbMovieEntity>()
            {
                CreateTestFakeDbMovie(title)
            };
            _fakedbRepo.GetMoviesAsync().Returns(movies);

            var result = await _sut.GetMoviesAsync(dbName);

            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                await _fakedbRepo.Received(1).GetMoviesAsync();
            });
        }

        [Test]
        public async Task WhenGetMovieByTitleAsyncWithOmDbIsCalled_ThenProperResultIsReturned()
        {
            const string title = "movie title";
            const string dbName = "omdb";

            var movie = CreateTestOmDbMovie(title);
            _omdbRepo.GetMovieByTitleAsync(title).Returns(movie);

            var result = await _sut.GetMovieByTitleAsync(dbName, title);

            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                await _omdbRepo.Received(1).GetMovieByTitleAsync(title);
            });
        }

        [Test]
        public async Task WhenGetMovieByTitleAsyncWithFakeDbIsCalled_ThenProperResultIsReturned()
        {
            const string title = "movie title";
            const string dbName = "fakedb";

            var movie = CreateTestFakeDbMovie(title);
            _fakedbRepo.GetMovieByTitleAsync(title).Returns(movie);

            var result = await _sut.GetMovieByTitleAsync(dbName, title);

            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                await _fakedbRepo.Received(1).GetMovieByTitleAsync(title);
            });
        }

        private OmDbMovieEntity CreateTestOmDbMovie(string title)
        {
            return new OmDbMovieEntity(title)
            {
                Id = 1,
                Year = "2020",
                Director = "Director",
                Genre = "Genre",
                Plot = "Plot"
            };
        }

        private FakeDbMovieEntity CreateTestFakeDbMovie(string title)
        {
            return new FakeDbMovieEntity()
            {
                Id = 1,
                MovieDetails = new List<FakeDbMovieDetailsEntity>
                    {
                        new FakeDbMovieDetailsEntity
                        {
                            Id = 2,
                            Title = title,
                            Year = "2010-2012",
                            Poster = "url",
                            FakeDbMovieId = 1
                        }
                    }
            };
        }
    }
}
