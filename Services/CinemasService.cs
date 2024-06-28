using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movies_ecommerce.Data;
using movies_ecommerce.Models;
using movies_ecommerce.Settings;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Services
{
    public class CinemasService : ICinemasService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _ImagePath;
        public CinemasService(AppDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _ImagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePathCinema}";
        }
        public Cinema? getByid(int id)
        {
            return _context.Cinemas.Find(id);
        }
        public IEnumerable<Cinema> GetAll()
        {
            return _context.Cinemas.AsNoTracking().ToList();
        }

        public IEnumerable<SelectListItem> GetCinemas()
        {
            return _context.Cinemas.Select(x=>new SelectListItem { Text = x.Name,Value = x.Id.ToString()})
                .OrderBy(x=>x.Text).AsNoTracking().ToList();
        }
       
        public async Task Add(AddActorViewModel model)
        {
            var Cinema = new Cinema();
            Cinema.Name = model.Name;
            Cinema.Description = model.Bio;
            Cinema.Logo = await savecover(model.Cover);
            await _context.AddAsync(Cinema);
            await _context.SaveChangesAsync();
        }

        public async Task<Cinema?> Edit(EditActorViewModel model)
        {
            var cinema = getByid(model.Id); 
            bool logo = model.Cover != null;
            if (cinema == null)
                return null;
            var oldlog = cinema.Logo; 
            cinema.Name = model.Name;
            cinema.Description = model.Bio;
            if(logo)
            {
                cinema.Logo = await savecover(model.Cover!);
            }
            var ex = await _context.SaveChangesAsync();
            if(ex>0)
            {
                if(logo)
                {
                    var oldlogo = Path.Combine(_ImagePath, oldlog);
                    File.Delete(oldlogo);
                }
                return cinema;
            }
            else
            {
                var newlogo = Path.Combine(_ImagePath , cinema.Logo);
                File.Delete(newlogo);
                return null;
            }

        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var cinema = getByid(id);
            if (cinema == null)
                return isDeleted;
            _context.Remove(cinema);
            var ex = _context.SaveChanges();
            if (ex > 0) 
            {
                var Cpath = Path.Combine(_ImagePath, cinema.Logo);
                File.Delete(Cpath);
                isDeleted = true;
            }


            return isDeleted;
           
        }
        private async Task<string> savecover(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var CoverPath = Path.Combine(_ImagePath, CoverName);

            using var stream = File.Create(CoverPath);
            await cover.CopyToAsync(stream);

            return CoverName;
        }

        public IEnumerable<Cinema> Search(string term)
        {
            return _context.Cinemas.Where(x=>x.Name.Contains(term)).AsNoTracking().ToList();
        }
    }
}
