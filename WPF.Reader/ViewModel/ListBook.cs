using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.ASP.Server;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public int PageNumber { get; set; } = 1;

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<BookLight> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        public ListBook()
        {
            Ioc.Default.GetRequiredService<LibraryService>().UpdateBookList();

            ItemSelectedCommand = new RelayCommand(book =>
            {
                Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(book);
            });

            NextCommand = new RelayCommand(next =>
            {
                if (PageNumber < 20)
                {
                    Ioc.Default.GetRequiredService<LibraryService>().NextBookListPage(PageNumber);
                    PageNumber++;
                }
            });

            PreviousCommand = new RelayCommand(previous =>
            {
                if (PageNumber > 1)
                {
                    PageNumber--;
                    Ioc.Default.GetRequiredService<LibraryService>().PreviousBookListPage(PageNumber);
                }
            });
        }
    }
}
