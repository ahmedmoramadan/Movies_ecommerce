namespace movies_ecommerce.Models
{
    public class Movie : BasicEntity1
    {
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        public ICollection<Actor_Movie> actors_Movies { get; set; }
    }
}
