using System;

namespace ReadingApp.Models
{
    public class Book
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public DateTime LastReadTime { get; set; } = DateTime.Now;
        public string FileName { get; set; } = string.Empty;
    }
} 