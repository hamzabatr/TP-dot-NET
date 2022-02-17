﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ASP.Server.Model
{
    public class Book
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Content { get; set; }
        public List<Genre> Genres { get; set; }



        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        // N'oublier pas qu'un livre peut avoir plusieur genres
    }

    public class BookLight
    {
        public Book Book { init; private get; }

        public string Name => Book.Name;
        public double Price => Book.Price;
        public int Id => Book.Id;
        public string Author => Book.Author;
        public List<Genre> Genre => Book.Genres;
    }
}
