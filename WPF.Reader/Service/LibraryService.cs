using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouer et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public LibraryService()
        {
            var SF = new Genre() { Name = "SF" };
            var Classic = new Genre() { Name = "Classic" };
            var Romance = new Genre() { Name = "Romance" };
            var Thriller = new Genre() { Name = "Thriller" };

            Genres = new ObservableCollection<Genre>()
            {
                SF,
                Classic,
                Romance,
                Thriller
            };

            Books = new ObservableCollection<Book>() {
                new() { Name = "123", Author = "Blabla1", Content = "Blablabla", Genres = new List<Genre> {SF, Thriller}},
                new() { Name = "234", Author = "Blabla2", Content = "Blablabla2", Genres = new List<Genre> {Romance, Classic}},
                new() { Name = "456", Author = "Blabla3", Content = "Blablabla3", Genres = new List<Genre> {SF, Thriller}}
            };
        }

        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<Genre> Genres { get; set; } 

        public async void UpdateBookList()
        {
            /*
            var httpClient = new HttpClient() { BaseAddress = new Uri("https://127.0.0.1:5001") };

            var books = await new ASP.Server.Client(httpClient).ApiBookGetBooksAsync(new List<int>() { 1});
            Books.Clear();
            foreach(var book in books.Select(x => new Book() { Name = x.Name }))
            {
                Books.Add(book);
            }
            */
        }

        // C'est aussi ici que vous ajouterez les requète réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
    }
}
