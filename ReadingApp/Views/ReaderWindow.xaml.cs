using ReadingApp.Models;
using ReadingApp.Services;
using ReadingApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace ReadingApp.Views
{
    public partial class ReaderWindow : Window
    {
        private readonly ReaderViewModel _viewModel;
        private const double BORDER_DETECTION_HEIGHT = 40; // 上下边框检测高度

        public ReaderWindow(Book book, IBookService bookService)
        {
            InitializeComponent();
            _viewModel = new ReaderViewModel(book, bookService);
            DataContext = _viewModel;
            
            // 确保窗口正确显示
            Loaded += (s, e) => {
                Activate();
                Focus();
                
                // 默认文本隐藏
                _viewModel.IsMouseOver = false;
                
                // 默认边框隐藏
                TopBorder.Opacity = 0;
                BottomBorder.Opacity = 0;
                
                // 确保窗口可以接收鼠标输入
                this.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
            };
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            double windowHeight = this.ActualHeight;
            
            // 检测是否在上边框区域
            if (position.Y <= BORDER_DETECTION_HEIGHT)
            {
                TopBorder.Opacity = 1;
            }
            else
            {
                TopBorder.Opacity = 0;
            }
            
            // 检测是否在下边框区域
            if (position.Y >= windowHeight - BORDER_DETECTION_HEIGHT)
            {
                BottomBorder.Opacity = 1;
            }
            else
            {
                BottomBorder.Opacity = 0;
            }
            
            // 检测是否在中间文本区域
            if (position.Y > BORDER_DETECTION_HEIGHT && position.Y < windowHeight - BORDER_DETECTION_HEIGHT)
            {
                _viewModel.IsMouseOver = true;
            }
            else
            {
                _viewModel.IsMouseOver = false;
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            // 鼠标离开窗口时隐藏所有内容
            _viewModel.IsMouseOver = false;
            TopBorder.Opacity = 0;
            BottomBorder.Opacity = 0;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Up || e.Key == Key.Left)
            {
                // 上一页
                if (_viewModel.PreviousPageCommand.CanExecute(null))
                {
                    _viewModel.PreviousPageCommand.Execute(null);
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Down || e.Key == Key.Right)
            {
                // 下一页
                if (_viewModel.NextPageCommand.CanExecute(null))
                {
                    _viewModel.NextPageCommand.Execute(null);
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // 关闭
                _viewModel.CloseCommand.Execute(null);
                e.Handled = true;
            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            // 只在当前页面内滚动，不自动翻页
            var scrollViewer = FindScrollViewer();
            if (scrollViewer != null)
            {
                // 正常滚动，速度调整为更合理的值
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta / 3);
                e.Handled = true;
            }
        }

        // 辅助方法：查找ScrollViewer控件
        private ScrollViewer FindScrollViewer()
        {
            return ContentScrollViewer;
        }
    }
} 