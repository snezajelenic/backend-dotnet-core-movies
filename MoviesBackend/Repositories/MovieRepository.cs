using System.Collections.Generic;
using System.Linq;
using MoviesBackend.Models;
using MoviesBackend.Repositories;

namespace MoviesBackend.WebApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private DatabaseContext _dbContext;

        public MovieRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _dbContext.Movies.AsEnumerable();
        }

        public Movie GetById(int id)
        {
            return _dbContext.Movies.FirstOrDefault(m => m.Id == id);
        }

        public Movie Create(Movie movie)
        {
            var result = _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public Movie Update(Movie movie)
        {
            _dbContext.Movies.Update(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public void Delete(Movie movie)
        {
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Movie> SearchWithPaging(int pageSize, int pageNumber, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.ToLower();
                var data = _dbContext.Movies.Where(m =>
                     m.Name.ToLower().Contains(value) ||
                     m.Genre.ToLower().Contains(value) ||
                     m.Director.ToLower().Contains(value) ||
                     m.Year.ToString().ToLower().Contains(value)).OrderBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return data;
            }
            return _dbContext.Movies.OrderBy(m => m.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public int TotalCount(string value)
        {
            value = value.ToLower();

            return _dbContext.Movies.Where(m =>
                     m.Name.ToLower().Contains(value) ||
                     m.Genre.ToLower().Contains(value) ||
                     m.Director.ToLower().Contains(value) ||
                     m.Year.ToString().ToLower().Contains(value)).Count();
        }

    }
}
