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
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>() {
            new Book() { Name = "123"},
            new Book() { Name = "234"},
            new Book() { Name = "456"}
        };

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
