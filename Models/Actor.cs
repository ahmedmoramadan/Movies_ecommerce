namespace movies_ecommerce.Models
{
    public class Actor : BasicEntity2
    {
      public ICollection<Actor_Movie> actors_Movies { get; set; } 
    }
}
