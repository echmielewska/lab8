using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels; // <-- To jest kluczowe!

// Dodajemy dziedziczenie po ObservableObject, skoro masz Toolkit
public partial class CardViewModel : ObservableObject
{
    // Ranga karty (np. "9", "10", "J", "Q", "K", "A")
    public string Rank { get; set; }

    // Kolor karty (np. "♠", "♣", "♥", "♦")
    public string Suit { get; set; }

    // Właściwość pomocnicza do koloru
    // W CardViewModel.cs
    public string CardColor => (Suit == "♥" || Suit == "♦") ? "Red" : "Black";

    public CardViewModel(string rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }
}