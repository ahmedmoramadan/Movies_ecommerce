using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies_ecommerce.Models;
using movies_ecommerce.Services;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Controllers
{
    [Authorize(Roles ="Admin,Producer")]//Producer
    public class ProducersController : Controller
    {
        public readonly IProducersService _producersService; 
        public ProducersController(IProducersService producersService) 
        {
            _producersService = producersService;
        }
        public IActionResult Index()
        {
            IEnumerable<Producer> producer = _producersService.GetAll();
            return View(producer);
        }
        [HttpGet]
        public IActionResult add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> add(AddActorViewModel addprod)
        {
            if(!ModelState.IsValid)
            {
                return View(addprod);
            }
            await _producersService.AddProducer(addprod);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var producers = _producersService.GetById(id);
            if (producers == null)
                return NotFound();
            EditActorViewModel model = new()
            {
                CuruntCover = producers.Cover,
                Name = producers.Name,
                Bio = producers.Bio,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditActorViewModel editprod)
        {
            if (!ModelState.IsValid)
            {
                return View(editprod);
            }
            var e= await _producersService.Edit(editprod);
            if (e == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var isDeleted = _producersService.Delete(id);
           return isDeleted ? Ok() : BadRequest();
        }
        public IActionResult Search(string term)
        {
            var search = _producersService.Search(term);
            return View(nameof(Index),search);
        }
    }
}
