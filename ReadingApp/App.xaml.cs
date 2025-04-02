using ReadingApp.Services;
using System.Windows;

namespace ReadingApp
{
    public partial class App : Application
    {
        public static IBookService BookService { get; private set; } = new BookService();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
} 