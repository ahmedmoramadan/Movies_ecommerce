namespace movies_ecommerce.Models
{
    public class Category : BasicEntity0 
    {
        public ICollection<Movie> Movies { get; set; }
    }
}
