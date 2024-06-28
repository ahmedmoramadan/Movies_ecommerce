using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movies_ecommerce.Data;
using movies_ecommerce.Models;
using movies_ecommerce.Settings;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Services
{
    public class ProducersService : IProducersService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _ImagePath;
        public ProducersService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _ImagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePathProducer}";
        }
        public IEnumerable<Producer> GetAll()
        {
            return _context.Producers.AsNoTracking().ToList();
        }

        public IEnumerable<SelectListItem> GetProducers()
        {
            return _context.Producers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
               .OrderBy(x=>x.Text).AsNoTracking().ToList();
        }

        public Producer? GetById(int id)
        {
            return _context.Producers.Find(id);
        }
       
        public async Task<Producer?> Edit(EditActorViewModel model)
        {
            var pro = GetById(model.Id);
            var photo = model.Cover != null;
            if (pro == null)
                return null!;
            var oldpp = pro.Cover;
            pro.Name = model.Name;
            pro.Bio = model.Bio;
            if(photo)
            {
                pro.Cover = await savephoto(model.Cover!);
            }
            var n = await _context.SaveChangesAsync();
            if (n > 0) 
            {
                if (photo) {
                    var old_p = Path.Combine(_ImagePath, oldpp);
                    File.Delete(old_p);
                }
                return pro;
            }
            else
            {
                var old_p = Path.Combine(_ImagePath,pro.Cover);
                File.Delete(old_p);
                return null;
            }
            

        }

        public async Task AddProducer(AddActorViewModel model)
        {
            var PRO_Photo = await savephoto(model.Cover);

            Producer p = new()
            {
                Cover = PRO_Photo,
                Name = model.Name,
                Bio = model.Bio,
            };
            await _context.Producers.AddAsync(p);
            await _context.SaveChangesAsync();
        }
      
        public bool Delete(int id)
        {
            var isDelete = false;
            var pdr = GetById(id);
            if (pdr == null)
                return isDelete;
            _context.Producers.Remove(pdr);
            var done = _context.SaveChanges();
            if(done > 0)
            {
                var cover = Path.Combine(_ImagePath, pdr.Cover);
                File.Delete(cover);
                isDelete = true;
            }
             return isDelete;
        }
        private async Task<string> savephoto(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_ImagePath, CoverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return CoverName;
        }

        public IEnumerable<Producer> Search(string term)
        {
            IEnumerable<Producer> Prod = _context.Producers.Where(x => x.Name.Contains(term)).AsNoTracking().ToList();
            return Prod;
        }
    }
}
