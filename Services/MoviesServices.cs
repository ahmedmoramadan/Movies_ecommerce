using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using movies_ecommerce.Data;
using movies_ecommerce.Models;

namespace movies_ecommerce.Services
{
    public class MoviesServices : IMoviesServices
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _ImagePath;
        public MoviesServices(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _ImagePath =$"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePathMovies}";
        }

        public Movie? GetById(int id)
        {
            return _context.Movies.Include(x => x.Category)
                .Include(x => x.Producer)
                 .Include(x => x.Cinema)
                .Include(x => x.actors_Movies)
                .ThenInclude(x => x.Actor)               
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies
                .Include(x=>x.Cinema)
                .Include(x=>x.Category)
                .Include(x=>x.Producer)
                .Include(x=>x.actors_Movies)
                .ThenInclude(x=>x.Actor)
                .ToList();
        }

        public IEnumerable<Movie> Search(string term)
        {
            IEnumerable<Movie> Prod = _context.Movies
                .Include(x=>x.Producer)
                .Include(x=>x.Category)
                .Include(x=>x.Cinema)
                .Include(x=>x.actors_Movies)
                .ThenInclude(x=>x.Actor)
                .Where(x => x.Name.Contains(term))
                .AsNoTracking().ToList();
            return Prod;
        }

        public async Task Add(MovieViewModel model)
        {
            var CoverName = await savecover(model.Cover);

            Movie movie = new Movie();
            movie.Name = model.Name;
            movie.Description = model.DeScription;
            movie.Price = model.price;
            movie.StartDate = model.StartDate;
            movie.EndDate = model.EndDate;
            movie.ProducerId = model.ProducerId;
            movie.CategoryId = model.CategoryId;
            movie.CinemaId = model.CinemaId;
         //   movie.actors_Movies=model.SelectActors.Select(x=>new Actor_Movie { ActorId = x}).ToList();
            movie.Cover = CoverName;

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();  
        }

        public async Task<Movie?> Edit(EditMovieViewModel model)
        {
            var movie = GetById(model.id);
            var cover = model.Cover != null;
            if (movie == null)
                return null!;
            var oldphoto = movie.Cover;
            movie.Name=model.Name;
            movie.Description = model.DeScription;
            movie.Price = model.price;
            movie.StartDate = model.StartDate;
            movie.EndDate = model.EndDate;
            movie.CinemaId=model.CinemaId;
            movie.ProducerId = model.ProducerId;
            movie.CategoryId=model.CategoryId;
            movie.actors_Movies = model.SelectActors.Select(x=>new Actor_Movie { ActorId = x}).ToList();
            movie.CategoryId =model.CategoryId;
            if(cover)
            {
                movie.Cover = await savecover(model.Cover!);
            }
            int n = _context.SaveChanges();
            if (n > 0) 
            {
                if(cover)
                {
                    var old = Path.Combine(_ImagePath, oldphoto);
                    File.Delete(old);
                }
                return movie;
            }
            else
            {
                var New =Path.Combine(_ImagePath , movie.Cover);
                File.Delete(New);
                return null;
            }
        }

        public  bool Delete(int id)
        {
            var movie=GetById(id);
            var IsDelete = false;
            if(movie==null)
                return false;
            _context.Movies.Remove(movie);
            var ex =  _context.SaveChanges();
            if (ex > 0)
            {
                var mpath=Path.Combine(_ImagePath, movie.Cover);
                File.Delete(mpath);
                IsDelete = true;
            }
            return IsDelete;
        }

        private async Task<string> savecover(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var CoverPath = Path.Combine(_ImagePath, CoverName);

            using var stream = File.Create(CoverPath);
            await cover.CopyToAsync(stream);

            return CoverName;
        }
    }
}
