using Microsoft.AspNetCore.Mvc.Rendering;
using movies_ecommerce.Attribute;
using System.ComponentModel.DataAnnotations;

namespace movies_ecommerce.ViewModel
{
    public class MovieViewModel:MainMovieViewModel
    {
        [Display(Name = "Movie Cover")]
        [AllowedExtentions(FileSettings.AllowedExtentions),
            MaxSize(FileSettings.MaxSizeinByte)]
        public IFormFile Cover { get; set; } = default!;

    }
}
