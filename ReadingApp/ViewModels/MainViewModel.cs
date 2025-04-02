using Microsoft.Win32;
using ReadingApp.Models;
using ReadingApp.Services;
using ReadingApp.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReadingApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IBookService _bookService;
        private ObservableCollection<Book> _books = new();
        private bool _isUploadTabSelected = true;
        private string _selectedFilePath = string.Empty;
        private Book? _selectedBook;

        public ObservableCollection<Book> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        public bool IsUploadTabSelected
        {
            get => _isUploadTabSelected;
            set => SetProperty(ref _isUploadTabSelected, value);
        }

        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set => SetProperty(ref _selectedFilePath, value);
        }

        public Book? SelectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value);
        }

        public ICommand SelectFileCommand { get; }
        public ICommand UploadFileCommand { get; }
        public ICommand OpenBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand SwitchToUploadTabCommand { get; }
        public ICommand SwitchToShelfTabCommand { get; }

        public MainViewModel(IBookService bookService)
        {
            _bookService = bookService;
            
            SelectFileCommand = new RelayCommand(_ => SelectFile());
            UploadFileCommand = new RelayCommand(_ => UploadFile(), _ => !string.IsNullOrEmpty(SelectedFilePath));
            OpenBookCommand = new RelayCommand(book => OpenBook(book as Book), book => book != null);
            DeleteBookCommand = new RelayCommand(book => DeleteBook(book as Book), book => book != null);
            SwitchToUploadTabCommand = new RelayCommand(_ => IsUploadTabSelected = true);
            SwitchToShelfTabCommand = new RelayCommand(_ => { IsUploadTabSelected = false; LoadBooks(); });
            
            // 加载书籍列表
            LoadBooks();
        }

        private async void LoadBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            Books = new ObservableCollection<Book>(books);
        }

        private void SelectFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "文本文件 (*.txt)|*.txt",
                Title = "选择要上传的小说文件"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
            }
        }

        private async void UploadFile()
        {
            if (string.IsNullOrEmpty(SelectedFilePath))
                return;

            if (!File.Exists(SelectedFilePath))
            {
                MessageBox.Show("文件不存在", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var book = await _bookService.AddBookAsync(SelectedFilePath);
                SelectedFilePath = string.Empty;
                IsUploadTabSelected = false;
                LoadBooks();
                MessageBox.Show("上传成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"上传失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenBook(Book? book)
        {
            if (book == null) return;

            try
            {
                var readerWindow = new ReaderWindow(book, _bookService);
                readerWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开书籍失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteBook(Book? book)
        {
            if (book == null) return;

            var result = MessageBox.Show($"确定要删除《{book.Title}》吗？", "确认删除", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                await _bookService.DeleteBookAsync(book.Id);
                LoadBooks();
            }
        }
    }
} 