using movies_ecommerce.Attribute;
using movies_ecommerce.Settings;
using System.ComponentModel.DataAnnotations;

namespace movies_ecommerce.ViewModel
{
    public class AddActorViewModel :ActorViewModel
    {
       
        [Display(Name = "Photo")]
        [AllowedExtentions(FileSettings.AllowedExtentions),
            MaxSize(FileSettings.MaxSizeinByte)]
        public IFormFile Cover { get; set; } = default!;
    }
}
