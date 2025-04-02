using ReadingApp.ViewModels;
using System.Windows;

namespace ReadingApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(App.BookService);
        }
    }
} 