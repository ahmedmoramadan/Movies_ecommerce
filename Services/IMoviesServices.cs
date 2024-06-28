using movies_ecommerce.Models;

namespace movies_ecommerce.Services
{
    public interface IMoviesServices
    {
        IEnumerable<Movie> GetAll();
        Movie? GetById(int id);
        IEnumerable<Movie> Search(string term);
        Task Add(MovieViewModel model);
        Task<Movie?> Edit(EditMovieViewModel model); 
        bool Delete(int id);
    }
}
