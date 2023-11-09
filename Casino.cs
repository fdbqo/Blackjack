using System;

namespace Casino
{
    public class CasinoValues
    {
        public static decimal multiplierBlackjackWin = 1.5m;
        public static decimal multplierBlackjackDraw = .1m;

    }

    public class Player
    {
        public decimal Wallet { get; private set; }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int GamesPlayed { get; set; }
        public decimal totalWon { get; set; }
        public decimal totalBet { get; set; }
        public string Name { get; set;}
        public decimal Bet { get; set;}
        public decimal startingBalance = 1000;

        public Player(decimal startingBalance, string name)
        {
            Wallet = startingBalance;
            Name = name;

        }
        public Player()
        {
            Wallet = startingBalance;
            Name = Name;
        }

        public void addMoney(decimal amount)
        {
            Wallet += amount;
        }

        public void subtractMoney(decimal amount)
        {
            if (amount > Wallet)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            Wallet -= amount;
        }

        public void Stats()
        {
            Console.WriteLine($"┌─────────────────────────────────────────────────────┐");
            Console.WriteLine($"│ Player Name          : {Name,-27}  │");
            Console.WriteLine($"│ Your current Balance : \u20AC{Wallet,-26:N}  │");
            Console.WriteLine($"├─────────────────────────────────────────────────────┤");
            Console.WriteLine($"│ Total Won / Lost     : \u20AC{totalWon,-26:N}  │");
            Console.WriteLine($"│ Total Bet            : \u20AC{totalBet,-26:N}  │");
            Console.WriteLine($"├─────────────────────────────────────────────────────┤");
            Console.WriteLine($"│ Games Played         : {GamesPlayed,-27}  │");
            Console.WriteLine($"│ Games Won            : {Wins,-27}  │");
            Console.WriteLine($"│ Games Lost           : {Losses,-27}  │");
            Console.WriteLine($"│ Games Drawn          : {Draws,-27}  │");
            Console.WriteLine($"└─────────────────────────────────────────────────────┘");
        }
    }
}