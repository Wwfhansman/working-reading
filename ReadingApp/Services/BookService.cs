using ReadingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ReadingApp.Services
{
    public class BookService : IBookService
    {
        private readonly string _dataFolder;
        private readonly string _booksJsonPath;
        private List<Book> _books = new();

        public BookService()
        {
            _dataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ReadingApp");
            
            _booksJsonPath = Path.Combine(_dataFolder, "books.json");
            
            // 确保目录存在
            if (!Directory.Exists(_dataFolder))
            {
                Directory.CreateDirectory(_dataFolder);
            }

            // 加载书籍数据
            LoadBooks();
        }

        private void LoadBooks()
        {
            if (File.Exists(_booksJsonPath))
            {
                try
                {
                    string json = File.ReadAllText(_booksJsonPath);
                    _books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载书籍数据失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    _books = new List<Book>();
                }
            }
        }

        private async Task SaveBooksAsync()
        {
            try
            {
                string json = JsonSerializer.Serialize(_books, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_booksJsonPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存书籍数据失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return _books.OrderByDescending(b => b.LastReadTime).ToList();
        }

        public async Task<Book?> GetBookByIdAsync(string id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public async Task<Book> AddBookAsync(string filePath)
        {
            // 解析文件名获取书名和作者
            string fileName = Path.GetFileName(filePath);
            string title = fileName;
            string author = "";

            // 尝试从文件名中解析书名和作者，格式如: 《斗破苍穹》-天蚕土豆.txt
            if (fileName.Contains('《') && fileName.Contains('》'))
            {
                int startIndex = fileName.IndexOf('《') + 1;
                int endIndex = fileName.IndexOf('》');
                if (startIndex < endIndex)
                {
                    title = fileName.Substring(startIndex, endIndex - startIndex);
                    
                    // 尝试解析作者
                    if (fileName.Contains('-'))
                    {
                        int authorStartIndex = fileName.IndexOf('-') + 1;
                        int authorEndIndex = fileName.LastIndexOf('.');
                        if (authorStartIndex < authorEndIndex)
                        {
                            author = fileName.Substring(authorStartIndex, authorEndIndex - authorStartIndex);
                        }
                    }
                }
            }

            // 计算总页数
            int totalPages = await CalculateTotalPagesAsync(filePath);

            var book = new Book
            {
                Title = title,
                Author = author,
                FilePath = filePath,
                FileName = fileName,
                TotalPages = totalPages,
                CurrentPage = 1,
                LastReadTime = DateTime.Now
            };

            _books.Add(book);
            await SaveBooksAsync();
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.CurrentPage = book.CurrentPage;
                existingBook.LastReadTime = DateTime.Now;
                await SaveBooksAsync();
            }
        }

        public async Task DeleteBookAsync(string id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
                await SaveBooksAsync();
            }
        }

        public async Task<List<TextPage>> GetBookPagesAsync(Book book)
        {
            var pages = new List<TextPage>();
            try
            {
                string text = await File.ReadAllTextAsync(book.FilePath);
                var textPages = SplitTextIntoPages(text);
                for (int i = 0; i < textPages.Count; i++)
                {
                    pages.Add(new TextPage
                    {
                        PageNumber = i + 1,
                        Content = textPages[i]
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取书籍内容失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return pages;
        }

        public async Task<TextPage?> GetPageAsync(Book book, int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > book.TotalPages)
                return null;

            try
            {
                string text = await File.ReadAllTextAsync(book.FilePath);
                var textPages = SplitTextIntoPages(text);
                if (pageNumber <= textPages.Count)
                {
                    return new TextPage
                    {
                        PageNumber = pageNumber,
                        Content = textPages[pageNumber - 1]
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取页面内容失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        private async Task<int> CalculateTotalPagesAsync(string filePath)
        {
            try
            {
                string text = await File.ReadAllTextAsync(filePath);
                var pages = SplitTextIntoPages(text);
                return pages.Count;
            }
            catch
            {
                return 0;
            }
        }

        private List<string> SplitTextIntoPages(string text)
        {
            // 简单实现：每1000个字符为一页
            // 在实际应用中，应该根据界面大小和字体大小动态计算
            const int charsPerPage = 1000;
            var pages = new List<string>();

            for (int i = 0; i < text.Length; i += charsPerPage)
            {
                int length = Math.Min(charsPerPage, text.Length - i);
                pages.Add(text.Substring(i, length));
            }

            return pages;
        }
    }
} 