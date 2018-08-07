using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesBackend.Models;
using MoviesBackend.WebApi.Repositories;



namespace MoviesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// returns All Movies from database
        /// </summary>
        /// <returns>List of All Movies</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetAll()
        {
            return _repository.GetAll()?.ToList();
        }

        /// <summary>
        /// returns Movie by ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One movie</returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            var movie = _repository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        /// <summary>
        ///     Creates a Movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>New Movie</returns>
        [HttpPost]
        public ActionResult<Movie> Post([FromBody]Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return  _repository.Create(movie);
        }

        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns>Edited movie</returns>
        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != movie.Id)
            {
                return BadRequest($"Id: {id} does not match Movie Id: {movie.Id}");
            }

            try
            {
                _repository.Update(movie);
            }
            catch
            {
                return BadRequest("Failed to update.");
            }
            return Ok(movie);
        }

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok or NotFound</returns>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var movie = _repository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            _repository.Delete(movie);
            return Ok();
        }

        [Route("{pageSize}/{pageNumber}/{value?}")]
        [HttpGet]
        public ActionResult SearchWithPaging(int pageSize, int pageNumber, string value = "")
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"Page size {pageSize} and/or page number {pageNumber} is invalid.");
            }

            //var data = _repository.SearchWithPaging(pageSize, pageNumber, value);

            var json = new
            {
                data = _repository.SearchWithPaging(pageSize, pageNumber, value),
                page = pageNumber,
                count = _repository.TotalCount(value),
                searchBy = value
            };

            if (json.count < 1)
            {
                return BadRequest($"No movies that contains {value} !!");
            }
            return Ok(json);

        }

    }
}