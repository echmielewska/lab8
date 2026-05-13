using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Ruletka
{
    public class FinalWindow : Window
    {
        private readonly List<Player> _players;
        private int _index = 0;
        private TextBlock _nameTxt;
        private TextBlock _balanceTxt;
        private TextBlock _statsTxt;

        public FinalWindow(List<Player> players)
        {
            _players = players ?? new List<Player>();

            Width = 420;
            Height = 220;
            Title = "Koniec gry";

            var root = new StackPanel { Margin = new Thickness(12), Spacing = 8 };
            _nameTxt = new TextBlock { FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center };
            _balanceTxt = new TextBlock { FontSize = 24, FontWeight = FontWeight.Bold, HorizontalAlignment = HorizontalAlignment.Center };
            _statsTxt = new TextBlock { TextWrapping = TextWrapping.Wrap };

            var btnPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, Spacing = 8 };
            var prev = new Button { Content = "Poprzedni", Width = 100 };
            var next = new Button { Content = "Następny", Width = 100 };
            var close = new Button { Content = "Zamknij", Width = 100 };

            prev.Click += (_, _) => ShowPrevious();
            next.Click += (_, _) => ShowNext();
            close.Click += (_, _) => Close();

            btnPanel.Children.Add(prev);
            btnPanel.Children.Add(next);
            btnPanel.Children.Add(close);

            root.Children.Add(new TextBlock { Text = "Podsumowanie graczy:", FontSize = 16 });
            root.Children.Add(_nameTxt);
            root.Children.Add(_balanceTxt);
            root.Children.Add(_statsTxt);
            root.Children.Add(btnPanel);

            Content = root;

            if (_players.Count > 0)
                DisplayPlayer(0);
        }

        private void DisplayPlayer(int idx)
        {
            if (idx < 0 || idx >= _players.Count) return;
            _index = idx;
            var p = _players[idx];
            _nameTxt.Text = p.Name;
            _balanceTxt.Text = p.Balance.ToString();
            _statsTxt.Text = $"Gry: {p.GamesPlayed}\nWygrane: {p.Wins} (suma: {p.TotalWon})\nPrzegrane: {p.Losses} (suma: {p.TotalLost})";
        }

        private void ShowNext()
        {
            var ni = (_index + 1) % Math.Max(1, _players.Count);
            DisplayPlayer(ni);
        }

        private void ShowPrevious()
        {
            var ni = (_index - 1 + _players.Count) % Math.Max(1, _players.Count);
            DisplayPlayer(ni);
        }
    }
}
