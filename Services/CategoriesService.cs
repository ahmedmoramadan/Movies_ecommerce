using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movies_ecommerce.Data;

namespace movies_ecommerce.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext _context;
        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetCategories()
        {
            return _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString() , Text = c.Name})
                .AsNoTracking().OrderBy(x=>x.Text).ToList();
        }
    }
}
