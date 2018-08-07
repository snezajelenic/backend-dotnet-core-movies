using System.Collections.Generic;
using MoviesBackend.Models;

namespace MoviesBackend.WebApi.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
        Movie Create(Movie movie);
        Movie Update(Movie movie);
        void Delete(Movie movie);
        IEnumerable<Movie> SearchWithPaging(int pageSize, int pageNumber, string value);
        int TotalCount(string value);
    }
}
