using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using movies_ecommerce.Models;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Services
{
    public interface ICinemasService
    {
        Cinema? getByid(int id);
        IEnumerable<Cinema> GetAll();
        IEnumerable<Cinema> Search(string term);
        IEnumerable<SelectListItem> GetCinemas();
        Task Add(AddActorViewModel model);
        Task<Cinema?> Edit(EditActorViewModel model);
        bool Delete(int id);
    }
}
