namespace movies_ecommerce.Models
{
    public class Producer :BasicEntity2
    {
      public ICollection<Movie> Movies { get; set; }
    }
}
