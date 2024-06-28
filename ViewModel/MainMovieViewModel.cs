using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace movies_ecommerce.ViewModel
{
    public class MainMovieViewModel 

    {
        [Display(Name ="Movie Cover")]
        public string Name { get; set; }
        public string DeScription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double price { get; set; }
        [Display(Name = "CinemaNAme")]
        public int CinemaId { get; set; }
        public IEnumerable<SelectListItem> Cinemas { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<int>? SelectActors { get; set; }
        public IEnumerable<SelectListItem> Actors { get; set; } = Enumerable.Empty<SelectListItem>();

        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public int ProducerId { get; set; }
        public IEnumerable<SelectListItem> Producers { get; set; } = Enumerable.Empty<SelectListItem>();
    }

}
