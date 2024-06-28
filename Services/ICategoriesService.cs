using Microsoft.AspNetCore.Mvc.Rendering;

namespace movies_ecommerce.Services
{
    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetCategories();
    }
}
