using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies_ecommerce.Services;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Controllers
{
   // [Authorize(Roles ="Admin,Manager")]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _cinemasService;
        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }
        public IActionResult Index()
        {
            var AllSinemas = _cinemasService.GetAll();
            return View(AllSinemas);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddActorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
             await _cinemasService.Add(model);

            return RedirectToAction(nameof(Index));
        }
        //             _actianService.add(modelView);
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var c = _cinemasService.getByid(id);
            if (c == null)
                return NotFound();
            EditActorViewModel model = new EditActorViewModel();
            model.Id = id;
            model.Name = c.Name;
            model.CuruntCover = c.Logo;
            model.Bio = c.Description;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditActorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var cinema = await _cinemasService.Edit(model);
            if(cinema == null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Search(string term)
        {
            var search = _cinemasService.Search(term);
            return View(nameof(Index), search);
        }
        public IActionResult Delete(int id)
        {
            var isdleted= _cinemasService.Delete(id);
            return isdleted? Ok() : BadRequest();
        }
    }
}
