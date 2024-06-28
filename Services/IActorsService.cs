using Microsoft.AspNetCore.Mvc.Rendering;
using movies_ecommerce.Models;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Services
{
    public interface IActorsService
    {
        IEnumerable<Actor> GetAll();
        IEnumerable<SelectListItem> GetActors();
        IEnumerable<Actor> Search(string term);
        Actor? GetById(int id);
        Task AddActor(AddActorViewModel model);
        Task<Actor?> Edit(EditActorViewModel model);
        bool Delete(int id);
    }
}
