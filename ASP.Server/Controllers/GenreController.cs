using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ASP.Server.Data;

namespace ASP.Server.Controllers
{
    public class CreateGenreModel
    {
        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }
    }

    public class ModifyGenreModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Nom")]

        public string Name { get; set; }
    }

    public class GenreController : Controller
    {
        private readonly LibraryDbContext _libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this._libraryDbContext = libraryDbContext;
        }

        // A vous de faire comme BookController.List mais pour les genres !
        public ActionResult<IEnumerable<Genre>> List()
        {
            var listGenres = _libraryDbContext.Genre.Include(g=> g.Books).ToList();
            return View(listGenres);
        }

        public ActionResult<CreateGenreModel> Create(CreateGenreModel genre)
        {
            // Le IsValid est True uniquement si tous les champs de CreateGenreModel marqués Required sont remplis
            if (!ModelState.IsValid) return View(new CreateGenreModel() { });
            _libraryDbContext.Add(new Genre() {Name=genre.Name});
            _libraryDbContext.SaveChanges();
            return View(new CreateGenreModel() {});
        }

        public ActionResult<ModifyGenreModel> Modify(ModifyGenreModel genre, long idToModify)
        {
            var genreToModify = _libraryDbContext.Genre.Single(genre => genre.Id == idToModify);

            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (!ModelState.IsValid)
                return View(new ModifyGenreModel() {Id = genreToModify.Id, Name = genreToModify.Name});
            genreToModify.Name = genre.Name;
            // Completer la création du genre avec toute les information nécéssaire que vous aurez ajoutees, et mettez la liste des genres récupérés de la base aussi
            _libraryDbContext.SaveChanges();
            return RedirectToAction("List", "Genre");
            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
        }

        public ActionResult<IEnumerable<Genre>> Delete(long id)
        {
            var genre = _libraryDbContext.Genre.Single(genre => genre.Id == id);
            _libraryDbContext.Genre.Remove(genre);
            _libraryDbContext.SaveChanges();
            return RedirectToAction("List", "Genre");
        }
    }
}
