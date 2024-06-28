using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using movies_ecommerce.Models;

namespace movies_ecommerce.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Actor_Movie>().HasKey(x => new { x.ActorId, x.MovieId });
            modelBuilder.Entity<Category>().HasData(new Category[] {
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Comedy" } ,
                new Category { Id = 3, Name = "Drama" },
                new Category { Id = 4, Name = "Documentary" },
                new Category { Id = 5, Name = "Horror"},
                new Category { Id = 6, Name = "Comedy"}
            });
        }
    }
}
