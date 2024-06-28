using Microsoft.AspNetCore.Mvc.Rendering;
using movies_ecommerce.Models;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Services
{
    public interface IProducersService
    {
        IEnumerable<Producer> GetAll();
        IEnumerable<Producer> Search(string term);
        IEnumerable<SelectListItem> GetProducers();
        Producer? GetById(int id);
        Task<Producer?> Edit(EditActorViewModel model);
        Task AddProducer(AddActorViewModel model);
        bool Delete(int id);    
       
    }
}
