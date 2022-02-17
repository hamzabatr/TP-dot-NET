using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.Controllers
{
    public class CreateGenreModel
    {
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }
    }

    public class ModifyGenreModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Nom")]

        public String Name { get; set; }
    }

    public class GenreController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        // A vous de faire comme BookController.List mais pour les genres !
        public ActionResult<IEnumerable<Genre>> List()
        {
            List<Genre> ListGenres=null;
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            if (libraryDbContext.Genre.ToList() != null)
            {
                 ListGenres = libraryDbContext.Genre.Include(g=> g.Books).ToList();
            }else
            {
                throw new Exception();
            }
            return View(ListGenres);
        }

        public ActionResult<CreateGenreModel> Create(CreateGenreModel genre)
        {
            // Le IsValid est True uniquement si tous les champs de CreateGenreModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                libraryDbContext.Add(new Genre() {Name=genre.Name});
                libraryDbContext.SaveChanges();
                return RedirectToAction("List", "Genre");

            }
            return View(new CreateGenreModel() {});
        }

        public ActionResult<ModifyGenreModel> Modify(ModifyGenreModel genre, long idtomodify)
        {
            var genretomodify = libraryDbContext.Genre.Single(genre => genre.Id == idtomodify);

            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                genretomodify.Name = genre.Name;
                // Completer la création du genre avec toute les information nécéssaire que vous aurez ajoutees, et mettez la liste des genres récupérés de la base aussi
                libraryDbContext.SaveChanges();
                return RedirectToAction("List", "Genre");
            }
            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new ModifyGenreModel() { Id = genretomodify.Id, Name = genretomodify.Name });
        }

        public ActionResult<IEnumerable<Genre>> Delete(long id)
        {
            Genre genre = libraryDbContext.Genre.Single(genre => genre.Id == id);
            libraryDbContext.Genre.Remove(genre);
            libraryDbContext.SaveChanges();
            return RedirectToAction("List", "Genre");
        }
    }
}
