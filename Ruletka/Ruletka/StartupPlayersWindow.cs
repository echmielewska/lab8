using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia;

namespace Ruletka
{
    // Simple code-only window to input two player names and starting balances
    public class StartupPlayersWindow : Window
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        private TextBox _p1Name;
        private TextBox _p2Name;
        private TextBox _p1Balance;
        private TextBox _p2Balance;

        public StartupPlayersWindow()
        {
            Width = 420;
            Height = 260;
            Title = "Nowi gracze";

            var root = new StackPanel { Margin = new Thickness(12), Spacing = 8 };

            var row1 = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
            _p1Name = new TextBox { Watermark = "Imię gracza 1", Width = 180 };
            _p1Balance = new TextBox { Text = "1000", Width = 120 };
            row1.Children.Add(_p1Name);
            row1.Children.Add(_p1Balance);

            var row2 = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
            _p2Name = new TextBox { Watermark = "Imię gracza 2", Width = 180 };
            _p2Balance = new TextBox { Text = "1000", Width = 120 };
            row2.Children.Add(_p2Name);
            row2.Children.Add(_p2Balance);

            var btn = new Button { Content = "Start", Width = 120 };
            btn.Click += (_, _) => OnStart();

            root.Children.Add(new TextBlock { Text = "Podaj imiona i salda dla dwóch graczy:" });
            root.Children.Add(row1);
            root.Children.Add(row2);
            root.Children.Add(btn);

            Content = root;
        }

        private void OnStart()
        {
            var name1 = string.IsNullOrWhiteSpace(_p1Name.Text) ? "Gracz 1" : _p1Name.Text;
            var name2 = string.IsNullOrWhiteSpace(_p2Name.Text) ? "Gracz 2" : _p2Name.Text;
            if (!int.TryParse(_p1Balance.Text, out int b1)) b1 = 1000;
            if (!int.TryParse(_p2Balance.Text, out int b2)) b2 = 1000;

            Player1 = new Player(name1, b1);
            Player2 = new Player(name2, b2);
            this.Close();
        }
    }
}
