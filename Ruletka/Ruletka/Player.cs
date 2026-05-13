namespace Ruletka
{
    public class Player
    {
        public string Name { get; set; }
        public int Balance { get; set; }

        // Statistics
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int TotalWon { get; set; }
        public int TotalLost { get; set; }

        public Player(string name, int balance)
        {
            Name = name;
            Balance = balance;
            GamesPlayed = 0;
            Wins = 0;
            Losses = 0;
            TotalWon = 0;
            TotalLost = 0;
        }

        public void RecordWin(int amount)
        {
            GamesPlayed++;
            Wins++;
            TotalWon += amount;
        }

        public void RecordLoss(int amount)
        {
            GamesPlayed++;
            Losses++;
            TotalLost += amount;
        }
    }
}
