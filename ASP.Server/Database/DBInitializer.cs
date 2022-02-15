using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller;
            bookDbContext.Genre.AddRange(
                SF = new Genre() {Name="SF"},
                Classic = new Genre(),
                Romance = new Genre(),
                Thriller = new Genre()
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book() { Name = "machin1", Author = "lepeper1", Price  =1.10, Content = "rien de spécial1", Genres = new() { SF, Romance}  } , 
                new Book() { Name = "machin2", Author = "lepeper2", Price = 1.10, Content = "rien de spécial2", Genres = new() { SF, Romance } },
                new Book() { Name = "machin3", Author = "lepeper3", Price = 1.10, Content = "rien de spécial3", Genres = new() { SF, Romance } },
                new Book() { Name = "machin4", Author = "lepeper4", Price = 1.10, Content = "rien de spécial4", Genres = new() { SF, Romance } }
            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}