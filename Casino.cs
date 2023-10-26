using System;

namespace Casino
{
    public class CasinoValues
    {
        public static decimal multiplierBlackjackWin = 1.5m;
        public static decimal multplierBlackjackDraw = .05m;

    }

    public class Player
    {
        public decimal Wallet { get; private set; }
        public string Name { get; set;}
        public decimal Bet { get; set;}
        public decimal startingBalance = 10000;

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
    }
}