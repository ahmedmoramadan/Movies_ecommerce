using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies_ecommerce.Models;
using movies_ecommerce.Services;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Controllers
{
   // [Authorize(Roles ="Admin,Actor")]//Actor
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorsService;
        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }
        public IActionResult Index()
        {
            var The_Actors = _actorsService.GetAll();
            return View(The_Actors);
        }
        public IActionResult Search(string term)
        {
            var search =_actorsService.Search(term);
            return View(nameof(Index), search);
        }
        [HttpGet]
        public IActionResult AddActor()
        {
            AddActorViewModel model = new AddActorViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActor(AddActorViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                AddActorViewModel moodel = new AddActorViewModel();
                return View(moodel);
            }
            await _actorsService.AddActor(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _actorsService.GetById(id);
            if(actor == null) 
                return NotFound();
            EditActorViewModel model = new()
            {
                Id = id,
                Name = actor.Name,
                Bio = actor.Bio,
                CuruntCover = actor.Cover
            };
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
            var actor= await _actorsService.Edit(model);
            if (actor == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var isDelete = _actorsService.Delete(id);
            return isDelete ? Ok() : BadRequest();
        }
    }
}
