using System;
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
        public string Nom { get; set; }
        public string Autheur { get; set; }
        public double prix { get; set; }
        public string contenu { get; set; }
        public List<Genre> Genre { get; set; }



        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        // N'oublier pas qu'un livre peut avoir plusieur genres
    }

    public class BookLight
    {
        public Book Book { init; private get; }

        public string Name => Book.Nom;
        public double Price => Book.prix;
        public List<Genre> Genre => Book.Genre;
    }
}
