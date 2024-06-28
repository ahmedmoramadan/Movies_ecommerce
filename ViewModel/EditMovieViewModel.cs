using movies_ecommerce.Attribute;

namespace movies_ecommerce.ViewModel
{
    public class EditMovieViewModel:MainMovieViewModel
    {
        public int id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtentions(FileSettings.AllowedExtentions),
            MaxSize(FileSettings.MaxSizeinByte)]
        public IFormFile? Cover { get; set; } = default;
    }
}
