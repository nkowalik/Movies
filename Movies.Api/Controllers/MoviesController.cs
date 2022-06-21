using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Models;
using Movies.Api.Repositories;

namespace Movies.Api.Controllers
{
    /// <summary>
    /// The controller for movies
    /// </summary>
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// A constructor for MoviesController
        /// </summary>
        /// <param name="moviesRepository">A movies repository</param>
        /// <param name="mapper">An auto mapper</param>
        public MoviesController(IMoviesRepository moviesRepository, IMapper mapper)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
        }

        [HttpGet("{source}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMoviesAsync(string source)
        {
            var movieEntities = await _moviesRepository.GetMoviesAsync(source);

            if (movieEntities == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(movieEntities));
        }

        [HttpGet("{source}/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieAsync(string source, string name)
        {
            var movieEntity = await _moviesRepository.GetMovieAsync(source, name);

            if (movieEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDto>(movieEntity));
        }
    }
}
