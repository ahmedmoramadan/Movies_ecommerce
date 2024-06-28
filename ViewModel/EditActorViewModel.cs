using movies_ecommerce.Attribute;
using movies_ecommerce.Settings;
using System.ComponentModel.DataAnnotations;

namespace movies_ecommerce.ViewModel
{
    public class EditActorViewModel :ActorViewModel
    {
        public int Id { get; set; }
        public string? CuruntCover { get; set; }
        [Display(Name = "Actor Photo")]
        [AllowedExtentions(FileSettings.AllowedExtentions),
            MaxSize(FileSettings.MaxSizeinByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
