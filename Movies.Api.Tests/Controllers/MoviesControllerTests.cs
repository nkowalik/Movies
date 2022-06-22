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

        private const string Title = "movie title";

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
            const string dbName = "omdb";

            var movies = new List<OmDbMovieEntity>()
            {
                CreateTestOmDbMovie(Title)
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
            const string dbName = "fakedb";

            var movies = new List<FakeDbMovieEntity>()
            {
                CreateTestFakeDbMovie(Title)
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
            const string dbName = "omdb";

            var movie = CreateTestOmDbMovie(Title);
            _omdbRepo.GetMovieByTitleAsync(Title).Returns(movie);

            var result = await _sut.GetMovieByTitleAsync(dbName, Title);

            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                await _omdbRepo.Received(1).GetMovieByTitleAsync(Title);
            });
        }

        [Test]
        public async Task WhenGetMovieByTitleAsyncWithFakeDbIsCalled_ThenProperResultIsReturned()
        {
            const string dbName = "fakedb";

            var movie = CreateTestFakeDbMovie(Title);
            _fakedbRepo.GetMovieByTitleAsync(Title).Returns(movie);

            var result = await _sut.GetMovieByTitleAsync(dbName, Title);

            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                await _fakedbRepo.Received(1).GetMovieByTitleAsync(Title);
            });
        }

        [Test]
        public async Task WhenDeleteMovieByTitleAsyncWithOmDbIsCalled_ThenProperMethodsAreCalled()
        {
            const string dbName = "omdb";

            var movie = CreateTestOmDbMovie(Title);
            _omdbRepo.GetMovieByIdAsync(movie.Id).Returns(movie);

            await _sut.DeleteMovieAsync(movie.Id, dbName);

            Assert.Multiple(async () =>
            {
                await _omdbRepo.Received(1).GetMovieByIdAsync(movie.Id);
                await _omdbRepo.Received(1).DeleteMovieAsync(movie);
            });
        }

        [Test]
        public async Task WhenDeleteMovieByTitleAsyncWithFakeDbIsCalled_ThenProperMethodsAreCalled()
        {
            const string dbName = "fakedb";

            var movie = CreateTestFakeDbMovie(Title);
            _fakedbRepo.GetMovieByIdAsync(movie.Id).Returns(movie);

            await _sut.DeleteMovieAsync(movie.Id, dbName);

            Assert.Multiple(async () =>
            {
                await _fakedbRepo.Received(1).GetMovieByIdAsync(movie.Id);
                await _fakedbRepo.Received(1).DeleteMovieAsync(movie);
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
                Search = new List<FakeDbMovieDetailsEntity>
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
