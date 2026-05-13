using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApplication1.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<CardViewModel> _player1Hand = new();
    [ObservableProperty] private ObservableCollection<CardViewModel> _player2Hand = new();
    [ObservableProperty] private ObservableCollection<CardViewModel> _tableStack = new();

    [ObservableProperty] private string _gameStatus = "Rozpoczynanie...";
    [ObservableProperty] private bool _isPlayer1Turn;
    [ObservableProperty] private bool _isGameActive = true;

    public IRelayCommand DrawCardsCommand { get; }

    public MainWindowViewModel()
    {
        DrawCardsCommand = new RelayCommand(DrawCards);
        InitializeGame();
    }

    private void InitializeGame()
    {
        // tworzenie talii
        var deck = new List<CardViewModel>();
        string[] suits = { "♠", "♣", "♥", "♦" };
        string[] ranks = { "9", "10", "J", "Q", "K", "A" };

        foreach (var s in suits)
            foreach (var r in ranks)
                deck.Add(new CardViewModel(r, s));

        // rozdawanie
        var rnd = new Random();
        var shuffled = deck.OrderBy(x => rnd.Next()).ToList();

        for (int i = 0; i < shuffled.Count; i++)
        {
            if (i < 12) Player1Hand.Add(shuffled[i]);
            else Player2Hand.Add(shuffled[i]);
        }

        // kto zaczyna
        var p1Has9 = Player1Hand.Any(c => c.Rank == "9" && c.Suit == "♣");
        IsPlayer1Turn = p1Has9;
        GameStatus = IsPlayer1Turn ? "Tura: Gracz 1 (zacznij 9♣)" : "Tura: Gracz 2 (zacznij 9♣)";
    }

    [RelayCommand]
    public void PlayCard(CardViewModel card)
    {
        if (card == null) return;

        // Sprawdzenie czyja tura
        var currentHand = IsPlayer1Turn ? Player1Hand : Player2Hand;
        if (!currentHand.Contains(card)) return;

        var topCard = TableStack.LastOrDefault();

        // Zasady kładzenia karty 
        if (topCard == null)
        {
            // Pierwsza karta
            if (card.Rank == "9" && card.Suit == "♣") ExecuteMove(card, currentHand);
        }
        else if (GetCardValue(card.Rank) >= GetCardValue(topCard.Rank))
        {
            ExecuteMove(card, currentHand);
        }
    }

    private void ExecuteMove(CardViewModel card, ObservableCollection<CardViewModel> currentHand)
    {
        TableStack.Add(card);
        currentHand.Remove(card);

        if (currentHand.Count == 0)
        {
            GameStatus = IsPlayer1Turn ? "WYGRAŁ GRACZ 1!" : "WYGRAŁ GRACZ 2!";
            _isGameActive = false;
            return;
        }

        // Zmiana tury
        IsPlayer1Turn = !IsPlayer1Turn;
        GameStatus = IsPlayer1Turn ? "Tura: Gracz 1" : "Tura: Gracz 2";
    }

    private void DrawCards()
    {
        if (TableStack.Count <= 1) return; // Nie można brać, gdy stół pusty (poza startową kartą)

        var currentHand = IsPlayer1Turn ? Player1Hand : Player2Hand;

        // Gracz dobiera 3 karty ze stosu
        for (int i = 0; i < 3 && TableStack.Count > 1; i++)
        {
            var card = TableStack.Last();
            currentHand.Add(card);
            TableStack.Remove(card);
        }

        // Sortowanie ręki i zmiana tury
        SortHand(currentHand);
        IsPlayer1Turn = !IsPlayer1Turn;
        GameStatus = IsPlayer1Turn ? "Tura: Gracz 1" : "Tura: Gracz 2";
    }

    private void SortHand(ObservableCollection<CardViewModel> hand)
    {
        var sorted = hand.OrderBy(c => GetCardValue(c.Rank)).ToList();
        hand.Clear();
        foreach (var c in sorted) hand.Add(c);
    }

    private int GetCardValue(string rank) => rank switch
    {
        "9" => 9,
        "10" => 10,
        "J" => 11,
        "Q" => 12,
        "K" => 13,
        "A" => 14,
        _ => 0
    };
}