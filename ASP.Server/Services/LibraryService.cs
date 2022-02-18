namespace ASP.Server.Services
{
    public class LibraryService
    {
        private static LibraryService _instance;
        public static LibraryService Instance => _instance ??= new LibraryService();

        // Ajouter ici toutes vos fonctions qui peuvent être accéder a différent endroit de votre programme
    }
}
