namespace lab8.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private string _greeting = "Welcome to Avalonia!";

        public string Greeting
        {
            get => _greeting;
            set => SetProperty(ref _greeting, value);
        }
    }
}
