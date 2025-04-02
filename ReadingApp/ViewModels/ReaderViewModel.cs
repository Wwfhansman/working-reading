using ReadingApp.Models;
using ReadingApp.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ReadingApp.ViewModels
{
    public class ReaderViewModel : ViewModelBase
    {
        private readonly IBookService _bookService;
        private Book _book;
        private TextPage? _currentPage;
        private string _pageText = string.Empty;
        private int _currentPageNumber = 1;
        private int _totalPages;
        private string _pageInputText = "1";
        private bool _isTopMost;
        private double _opacity = 0.0;
        private bool _isMouseOver = false;

        public Book Book
        {
            get => _book;
            set => SetProperty(ref _book, value);
        }

        public TextPage? CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public string PageText
        {
            get => _pageText;
            set => SetProperty(ref _pageText, value);
        }

        public int CurrentPageNumber
        {
            get => _currentPageNumber;
            set
            {
                if (SetProperty(ref _currentPageNumber, value))
                {
                    PageInputText = value.ToString();
                    LoadPage(value);
                }
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set => SetProperty(ref _totalPages, value);
        }

        public string PageInputText
        {
            get => _pageInputText;
            set => SetProperty(ref _pageInputText, value);
        }

        public bool IsTopMost
        {
            get => _isTopMost;
            set => SetProperty(ref _isTopMost, value);
        }

        public double Opacity
        {
            get => _opacity;
            set => SetProperty(ref _opacity, value);
        }

        public bool IsMouseOver
        {
            get => _isMouseOver;
            set
            {
                if (SetProperty(ref _isMouseOver, value))
                {
                    // 修正逻辑：当鼠标悬停时显示文本，否则隐藏
                    UpdateOpacity();
                }
            }
        }

        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand JumpToPageCommand { get; }
        public ICommand ToggleTopMostCommand { get; }
        public ICommand CloseCommand { get; }

        public ReaderViewModel(Book book, IBookService bookService)
        {
            _book = book;
            _bookService = bookService;
            _currentPageNumber = book.CurrentPage;
            _totalPages = book.TotalPages;
            _pageInputText = book.CurrentPage.ToString();
            _isMouseOver = true;
            _opacity = 1.0;

            PreviousPageCommand = new RelayCommand(_ => PreviousPage(), _ => CurrentPageNumber > 1);
            NextPageCommand = new RelayCommand(_ => NextPage(), _ => CurrentPageNumber < TotalPages);
            JumpToPageCommand = new RelayCommand(_ => JumpToPage());
            ToggleTopMostCommand = new RelayCommand(_ => IsTopMost = !IsTopMost);
            CloseCommand = new RelayCommand(_ => CloseReader());

            // 加载当前页
            LoadPage(CurrentPageNumber);
        }

        private async void LoadPage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > TotalPages)
                return;

            try
            {
                var page = await _bookService.GetPageAsync(Book, pageNumber);
                if (page != null)
                {
                    CurrentPage = page;
                    PageText = page.Content;
                    
                    // 更新阅读进度
                    Book.CurrentPage = pageNumber;
                    await _bookService.UpdateBookAsync(Book);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载页面失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PreviousPage()
        {
            if (CurrentPageNumber > 1)
            {
                CurrentPageNumber--;
            }
        }

        private void NextPage()
        {
            if (CurrentPageNumber < TotalPages)
            {
                CurrentPageNumber++;
            }
        }

        private void JumpToPage()
        {
            if (int.TryParse(PageInputText, out int pageNumber))
            {
                if (pageNumber >= 1 && pageNumber <= TotalPages)
                {
                    CurrentPageNumber = pageNumber;
                }
                else
                {
                    MessageBox.Show($"页码必须在1到{TotalPages}之间", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    PageInputText = CurrentPageNumber.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入有效的页码", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                PageInputText = CurrentPageNumber.ToString();
            }
        }

        private void CloseReader()
        {
            // 保存阅读进度
            Task.Run(async () => await _bookService.UpdateBookAsync(Book));
            
            // 关闭窗口的逻辑将在View中实现
            Application.Current.Windows[1].Close();
        }

        private void UpdateOpacity()
        {
            // 修正逻辑：当鼠标悬停时显示文本，否则隐藏
            Opacity = IsMouseOver ? 1.0 : 0.0;
        }
    }
} 