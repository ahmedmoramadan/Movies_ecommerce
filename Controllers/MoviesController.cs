using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies_ecommerce.Models;
using movies_ecommerce.Services;

namespace movies_ecommerce.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MoviesController : Controller
    {
        private readonly IActorsService _actorsService;
        private readonly IProducersService _producersService;
        private readonly ICinemasService _cinemasService;
        private readonly IMoviesServices _moviesServices;
        private readonly ICategoriesService _categoriesService;
        public MoviesController(IActorsService actorsService,
            ICinemasService cinemasService, IProducersService producersService,
            IMoviesServices moviesServices, ICategoriesService categoriesService)
        {
            _actorsService = actorsService;
            _producersService = producersService;
            _moviesServices = moviesServices;
            _cinemasService = cinemasService;
            _categoriesService = categoriesService;
        }
        public IActionResult Index()
        {
            var model = _moviesServices.GetAll();
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            MovieViewModel model = new()
            {
                Producers = _producersService.GetProducers(),
                Categories = _categoriesService.GetCategories(),
                Actors = _actorsService.GetActors(),
                Cinemas = _cinemasService.GetCinemas(),

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                MovieViewModel Moviemodel = new MovieViewModel();
                model.Producers = _producersService.GetProducers();
                model.Categories = _categoriesService.GetCategories();
                model.Actors = _actorsService.GetActors();
                model.Cinemas = _cinemasService.GetCinemas();
                return View(Moviemodel);
            }
            await _moviesServices.Add(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var M = _moviesServices.GetById(id);
            if (M == null)
                return NotFound();
            EditMovieViewModel model = new EditMovieViewModel();
            model.id = id;
            model.DeScription = M.Description;
            model.Name = M.Name;
            model.price = M.Price;
            model.CurrentCover = M.Cover;
            model.StartDate = M.StartDate;
            model.EndDate = M.EndDate;
            model.CategoryId = M.CategoryId;
            model.CinemaId = M.CinemaId;
            model.ProducerId = M.ProducerId;
            model.SelectActors = M.actors_Movies.Select(x => x.ActorId).ToList();
            model.Cinemas = _cinemasService.GetCinemas().ToList();
            model.Categories = _categoriesService.GetCategories().ToList();
            model.Actors = _actorsService.GetActors().ToList();
            model.Producers = _producersService.GetProducers().ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMovieViewModel MV)
        {
            if (!ModelState.IsValid)
            {
                EditMovieViewModel model = new();
                model.Cinemas = _cinemasService.GetCinemas().ToList();
                model.Categories = _categoriesService.GetCategories().ToList();
                model.Actors = _actorsService.GetActors().ToList();
                model.Producers = _producersService.GetProducers().ToList();
                return View(model);
            }
            var returnmovie = await _moviesServices.Edit(MV);
            if (returnmovie == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
         public IActionResult Delete(int id)
        {
            bool movie = _moviesServices.Delete(id);
            

            return movie ? Ok() : BadRequest();

        }
        public IActionResult Search(string term)
        {
            var search = _moviesServices.Search(term);
            return View(nameof(Index), search);
        }
    }
}
