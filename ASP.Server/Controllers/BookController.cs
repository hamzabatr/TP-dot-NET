﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ASP.Server.Data;

namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        public string Author { get; set; }
        public double Price { get; set; }
        public string Content { get; set; }

        // Liste des genres séléctionné par l'utilisateur
        public List<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }
    }

    public class ModifyBookModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre
        public string Author { get; set; }
        public double Price { get; set; }
        public string Content { get; set; }

        // Liste des genres séléctionné par l'utilisateur
        public List<int> Genres { get; set; }

        public List<Genre> GenresList { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }
    }

    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            var listBooks = libraryDbContext.Books.Include(book => book.Genres).OrderBy(book => book.Id).ToList();

            return View(listBooks);
        }

        public ActionResult<CreateBookModel> Create(CreateBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (!ModelState.IsValid) return View(new CreateBookModel() {AllGenres = libraryDbContext.Genre.ToList()});
            // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
            var genres = libraryDbContext.Genre.Where(genre => book.Genres.Contains(genre.Id)).ToList();
            // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
            libraryDbContext.Add(new Book()
            {
                Name = book.Name, Author = book.Author, Price = book.Price, Content = book.Content, Genres = genres
            });
            libraryDbContext.SaveChanges();

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return RedirectToAction("List", "Book");
        }

        public ActionResult<ModifyBookModel> Modify(ModifyBookModel book, long idToModify)
        {
            var bookToModify = libraryDbContext.Books.Single(book => book.Id == idToModify);

            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (!ModelState.IsValid)
                return View(new ModifyBookModel()
                {
                    Id = bookToModify.Id, Name = bookToModify.Name, GenresList = bookToModify.Genres,
                    Author = bookToModify.Author, Price = bookToModify.Price, Content = bookToModify.Content,
                    AllGenres = libraryDbContext.Genre.ToList()
                });
            bookToModify.Name = book.Name;
            bookToModify.Author = book.Author;
            bookToModify.Content = book.Content;
            bookToModify.Price = book.Price;
            // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
            var genres = libraryDbContext.Genre.Where(genre => book.Genres.Contains(genre.Id)).ToList();
            // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
            libraryDbContext.SaveChanges();

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les sélectionner
            return RedirectToAction("List", "Book");
        }

        public ActionResult<IEnumerable<Book>> Delete(long id)
        {
            var book = libraryDbContext.Books.Single(book => book.Id == id);
            libraryDbContext.Books.Remove(book);
            libraryDbContext.SaveChanges();
            return RedirectToAction("List", "Book");
        }
    }
}