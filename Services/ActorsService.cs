using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using movies_ecommerce.Data;
using movies_ecommerce.Models;
using movies_ecommerce.Settings;
using movies_ecommerce.ViewModel;

namespace movies_ecommerce.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly string _ImagePath;
    
        public ActorsService(AppDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _ImagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePathActor}";
        }
        //function to handle the 
        public IEnumerable<SelectListItem> GetActors()
        {
           return _context.Actors.Select(x=>new SelectListItem{Value= x.Id.ToString() , Text=x.Name  })
                .AsNoTracking().OrderBy(x=>x.Text).ToList();
        }
        public IEnumerable<Actor> GetAll()
        {
            return _context.Actors.ToList();
        }
        public Actor? GetById(int id)
        {
            return _context.Actors.Find(id);
        }
        
        public async Task AddActor(AddActorViewModel model)
        {
            var CoverName = await savephoto(model.Cover);
           
            Actor actor = new Actor();
            actor.Name = model.Name;
            actor.Cover = CoverName;
            actor.Bio = model.Bio;
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Actor?> Edit(EditActorViewModel model)
        {
            var Actor = GetById(model.Id);
            var Photo = model.Cover !=null;
            if (Actor == null)
                return null;
            var oldcover = Actor!.Cover;

            Actor.Name = model.Name;
            Actor.Bio = model.Bio;
            if(Photo)
            {
                Actor.Cover = await savephoto(model.Cover!);
            }
            var n = _context.SaveChanges();
            if (n > 0)
            {
                if (Photo) {
                    var cover = Path.Combine(_ImagePath, oldcover);
                    File.Delete(cover);
                }
                return Actor;
            }
            else
            {
                var cover = Path.Combine(_ImagePath, Actor.Cover);
                File.Delete(cover);
                return null;
            }
        }
        
        public bool Delete(int id)
        {
            var IsDeleted = false;
            var actor = GetById(id);
            if (actor == null) 
                return IsDeleted;
            _context.Remove(actor);
            var ex =  _context.SaveChanges();
            if (ex > 0)
            {
                var cover = Path.Combine(_ImagePath, actor.Cover);
                File.Delete(cover);
                IsDeleted = true;
            }
            return IsDeleted;
        }
        public IEnumerable<Actor> Search(string term)
        {
            return _context.Actors.Where(x => x.Name.Contains(term)).AsNoTracking().ToList();
        }
        private async Task<string> savephoto(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine (_ImagePath , CoverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync (stream);
            return CoverName;
        }

       
    }
}
