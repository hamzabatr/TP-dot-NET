using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.ASP.Server;


namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; } = new RelayCommand(book => { Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(book); });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public BookLight CurrentBook { get; init; }

        public DetailsBook(BookLight book)
        {
            CurrentBook = book;
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new BookLight() { Name = "Test Book" }) { }
    }
}
