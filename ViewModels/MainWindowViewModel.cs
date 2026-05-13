using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApplication1.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<CardViewModel> _playerHand = new();

    [ObservableProperty]
    private ObservableCollection<CardViewModel> _tableStack = new();

    // Ręczna definicja komendy, aby uniknąć błędów generatora ze zdjęcia image_ea501d.png
    public IRelayCommand DrawCardsCommand { get; }

    public MainWindowViewModel()
    {
        // Inicjalizacja komendy
        DrawCardsCommand = new RelayCommand(DrawCards);

        InitializeDeck();
    }

    private void InitializeDeck()
    {
        var deck = new List<CardViewModel>();
        string[] suits = { "♠", "♣", "♥", "♦" };
        string[] ranks = { "9", "10", "J", "Q", "K", "A" };

        foreach (var s in suits)
            foreach (var r in ranks)
                deck.Add(new CardViewModel(r, s));

        var rnd = new Random();
        var shuffled = deck.OrderBy(x => rnd.Next()).ToList();

        PlayerHand = new ObservableCollection<CardViewModel>(shuffled);

        // Zasada Pana: jeśli masz 9 trefl, ląduje ona na stosie jako pierwsza
        var nineOfClubs = PlayerHand.FirstOrDefault(c => c.Rank == "9" && c.Suit == "♣");
        if (nineOfClubs != null)
        {
            PlayerHand.Remove(nineOfClubs);
            TableStack.Add(nineOfClubs);
        }
    }

    [RelayCommand]
    public void PlayCard(CardViewModel card)
    {
        if (card == null) return;

        // Proste sprawdzanie zasad: czy karta jest większa lub równa tej na stole?
        var topCard = TableStack.LastOrDefault();
        if (topCard == null || GetCardValue(card.Rank) >= GetCardValue(topCard.Rank))
        {
            TableStack.Add(card);
            PlayerHand.Remove(card);

            if (TableStack.Count > 4)
            {
                TableStack.RemoveAt(0);
            }
        }
    }

    public void DrawCards()
    {
        int cardsToDraw = 3;
        for (int i = 0; i < cardsToDraw; i++)
        {
            // Zostawiamy jedną kartę na stosie, żeby było na czym grać dalej
            if (TableStack.Count > 1)
            {
                var card = TableStack.Last();
                PlayerHand.Add(card);
                TableStack.Remove(card);
            }
        }

        // Sortowanie ręki po dobraniu, żeby łatwiej było planować ruchy
        var sorted = PlayerHand.OrderBy(c => GetCardValue(c.Rank)).ToList();
        PlayerHand.Clear();
        foreach (var c in sorted) PlayerHand.Add(c);
    }

    private int GetCardValue(string rank)
    {
        return rank switch
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
}