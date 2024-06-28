using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using movies_ecommerce.Data;
using movies_ecommerce.Models;
using movies_ecommerce.Services;

namespace movies_ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var con = builder.Configuration.GetConnectionString("Default") ??
               throw new InvalidOperationException("not database found");
           
            builder.Services.AddDbContext<AppDbContext>(option=> option.UseSqlServer(con));
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IActorsService , ActorsService>();
            builder.Services.AddScoped<IProducersService , ProducersService>();
            builder.Services.AddScoped<ICinemasService , CinemasService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<IMoviesServices, MoviesServices>();
            builder.Services.AddIdentity<AppUser , IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}