using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using WPF.Reader.ASP.Server;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://127.0.0.1:5001") };

        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouer et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public LibraryService()
        {
            UpdateGenreList();
            UpdateBookList();
        }

        public ObservableCollection<BookLight> Books { get; set; } = new ObservableCollection<BookLight>();

        public ObservableCollection<Genre> Genres { get; set; } = new ObservableCollection<Genre>();

        public async void UpdateBookList()
        {
            var books = await new Client(_httpClient).ApiBookGetBooksAsync(null, null);
            Books?.Clear();

            foreach (var book in books.OrderBy(book => book.Name))
            {
                Books.Add(book);
            }
        }

        public async void UpdateGenreList()
        {
            var genres = await new Client(_httpClient).ApiBookGetGenresAsync();
            Genres?.Clear();
            foreach (var genre in genres.Select(genre => new Genre() {Name = genre.Name, Id = genre.Id}))
            {
                Genres.Add(genre);
            }
        }

        // C'est aussi ici que vous ajouterez les requète réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
    }
}