using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels; 


public partial class CardViewModel : ObservableObject
{

    public string Rank { get; set; }


    public string Suit { get; set; }

 
    public string CardColor => (Suit == "♥" || Suit == "♦") ? "Red" : "Black";

    public CardViewModel(string rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }
}