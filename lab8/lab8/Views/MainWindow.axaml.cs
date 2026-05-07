using Avalonia.Controls;
using Avalonia.Interactivity;

namespace lab8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenGame1(object? sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.Greeting = "Otwieranie gry 1...";
            }
        }

        private void OpenGame2(object? sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.Greeting = "Otwieranie gry 2...";
            }
        }

        private void OpenGame3(object? sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.Greeting = "Otwieranie gry 3...";
            }
        }
    }
}