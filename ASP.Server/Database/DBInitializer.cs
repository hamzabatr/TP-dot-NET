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

            Genre Roman, SF, Classic, Romance, Thriller;
            bookDbContext.Genre.AddRange(
                Roman = new Genre() {Name = "Roman"},
                SF = new Genre() {Name = "SF"},
                Classic = new Genre() {Name = "Classic"},
                Romance = new Genre() {Name = "Romance"},
                Thriller = new Genre() {Name = "Thriller"}
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                var j = random.Next(2, bookDbContext.Genre.Count() + 1);
                bookDbContext.Books.Add(
                    new Book()
                    {
                        Name = i + " : Les titres qu'on peut incrémenter, c'est génial !",
                        Author = "Les auteurs aussi : " + i,
                        Price = i * i,
                        Content =
                            "Lorem ipsum dolor sit amet. Id ipsum tempore ab minima dolor nam totam asperiores in voluptas dignissimos et sequi obcaecati. Ad voluptas repellendus ut eaque voluptatum ut tempora voluptatem non tenetur voluptatem est quaerat quisquam ut vitae molestiae. Qui Quis praesentium est repellat debitis et omnis non repudiandae accusamus id sunt autem. Qui Quis nesciunt qui enim reiciendis et obcaecati rerum. Et architecto debitis et officiis perferendis sed quia dolorum ab assumenda doloribus rem quia debitis aut incidunt sequi. Ut molestiae quaerat 33 ducimus distinctio est praesentium dolores sed necessitatibus dolores et officia neque. Cum eligendi dolorem ea cumque veritatis qui porro illum aut voluptas similique vel itaque laborum aut quia laboriosam.",
                        Genres = new List<Genre> {bookDbContext.Genre.Single(genre => genre.Id == 1), bookDbContext.Genre.Single(genre => genre.Id == j)}
                    });
            }
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}