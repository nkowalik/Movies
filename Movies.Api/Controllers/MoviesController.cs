using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Models;
using Movies.Api.Infrastructure.Repositories;
using Movies.Api.Infrastructure.Entities;

namespace Movies.Api.Controllers
{
    /// <summary>
    /// The controller for movies data
    /// </summary>
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository<OmDbMovieEntity> _omDbMoviesRepository;
        private readonly IMoviesRepository<FakeDbMovieEntity> _fakeDbMoviesRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// A constructor for MoviesController
        /// </summary>
        /// <param name="omDbMoviesRepository">OmDb movies repository</param>
        /// <param name="fakeDbMoviesRepository">FakeDb movies repository</param>
        /// <param name="mapper">An auto mapper</param>
        public MoviesController(IMoviesRepository<OmDbMovieEntity> omDbMoviesRepository,
            IMoviesRepository<FakeDbMovieEntity> fakeDbMoviesRepository,
            IMapper mapper)
        {
            _omDbMoviesRepository = omDbMoviesRepository;
            _fakeDbMoviesRepository = fakeDbMoviesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all movies data from database by source
        /// </summary>
        /// <param name="source">A source from which the data will be collected. Supported sources: OmDb, FakeDb.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">All movies are returned</response>
        [HttpGet("{source}")]
        public async Task<IActionResult> GetMoviesAsync(string source)
        {
            return source.ToLower() switch
            {
                "omdb" => Ok(await _omDbMoviesRepository.GetMoviesAsync()),
                "fakedb" => Ok(await _fakeDbMoviesRepository.GetMoviesAsync()),
                _ => throw new ArgumentOutOfRangeException(nameof(source),
                           $"Invalid source {source}. Supported sources: OmDb, FakeDb."),
            };
        }

        /// <summary>
        /// Get a movie data by its title from requested source
        /// </summary>
        /// <param name="source">A source from which the data will be collected. Supported sources: OmDb, FakeDb.</param>
        /// <param name="title">A movie title (for FakeDb e.g. Batman)</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">A movie by title from the requested source is returned</response>
        /// <response code="404">No such movie was found in the requested source</response>
        [HttpGet("{source}/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByTitleAsync(string source, string title)
        {
            switch (source.ToLower())
            {
                case "omdb":
                    var movieOmDb = await _omDbMoviesRepository.GetMovieByTitleAsync(title);
                    return movieOmDb == null
                        ? NotFound()
                        : Ok(_mapper.Map<OmDbMovieDto>(movieOmDb));
                case "fakedb":
                    var movieFakeDb = await _fakeDbMoviesRepository.GetMovieByTitleAsync(title);                    
                    return movieFakeDb == null 
                        ? NotFound() 
                        : Ok(_mapper.Map<FakeDbMovieDto>(movieFakeDb));
                default:
                    throw new ArgumentOutOfRangeException(nameof(source),
                           $"Invalid source {source}. Supported sources: OmDb, FakeDb.");
            }            
        }

        /// <summary>
        /// Delete movie by id
        /// </summary>
        /// <param name="id">An id of the movie</param>
        /// <param name="source">A source from which the data will be deleted. Supported sources: OmDb, FakeDb.</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">A requested movie is deleted</response>
        [HttpDelete]
        public async Task<ActionResult> DeleteMovieAsync(int id, string source)
        {
            switch (source.ToLower())
            {
                case "omdb":
                    var moviesOmDb = await _omDbMoviesRepository.GetMovieByIdAsync(id);
                    if (moviesOmDb == null) break;
                    await _omDbMoviesRepository.DeleteMovieAsync(moviesOmDb);
                    return NoContent();
                case "fakedb":
                    var moviesFakeDb = await _fakeDbMoviesRepository.GetMovieByIdAsync(id);
                    if (moviesFakeDb == null) break;
                    await _fakeDbMoviesRepository.DeleteMovieAsync(moviesFakeDb);
                    return NoContent();
            }

            return NotFound();
        }
    }
}
