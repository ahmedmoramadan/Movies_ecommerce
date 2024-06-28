namespace movies_ecommerce.Models
{
    public class Cinema : BasicEntity0
    {
        public string Logo { get; set; } 
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
