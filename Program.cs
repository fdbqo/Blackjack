using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CardGen;
using Casino;

using System.Security.Cryptography;
using System.Runtime.InteropServices;


namespace Blackjack
{
    class Program
    {
        public static bool dealerDone = false;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Player player = new Player();

            Card.playerTotal = 0;
            Card.dealerTotal = 0;
            Card.gameRound = 0;


            Card deckCards = new Card();
            List<Card> deck = deckCards.DeckGen();
            deck = initialize(deck);

            foreach (Card c in deck)
            {
                Console.WriteLine(c.fullCard);
            }
            System.Console.WriteLine($"total cards in deck: {deck.Count}");



            Console.WriteLine("┌─────────────────────────────────────────────┐");
            Console.WriteLine("│ Welcome to Blackjack!                       │");
            Console.WriteLine("│ Enter your name!                            │");
            Console.WriteLine("└─────────────────────────────────────────────┘");
            Console.Write("Input: ");
            player.Name = Console.ReadLine();

            

            int menuInput = 0;



            while (menuInput != 3)
            {
                Console.WriteLine("┌─────────────────────────────────────────────┐");
                Console.WriteLine("│ Menu                                        │");
                Console.WriteLine("├─────────────────────────────────────────────┤");
                Console.WriteLine("│                                             │");
                Console.Write("│  (1) Play game                              │\n");
                Console.Write("│  (2) View Stats                             │\n");
                Console.Write("│  (3) Exit                                   │\n");
                Console.WriteLine("│                                             │");
                Console.WriteLine("└─────────────────────────────────────────────┘");
                Console.Write("Input: ");
                menuInput = int.Parse(Console.ReadLine());

                switch (menuInput)
                {
                    case 1:

                        Card.playerTotal = 0;
                        Card.dealerTotal = 0;
                        Card.gameRound = 0;
                        dealerDone = false;

                        deckCards = new Card();
                        deck = deckCards.DeckGen();
                        deck = initialize(deck);

                        Console.WriteLine($"┌─────────────────────────────────────────────────────┐");
                        Console.WriteLine($"│ Your current Balance :  \u20AC{player.Wallet,-19:N2}        │");
                        Console.WriteLine($"| How much do you want to bet? :                      │");
                        Console.WriteLine($"└─────────────────────────────────────────────────────┘");
                        Console.Write("Input: ");
                        player.Bet = Convert.ToDecimal(Console.ReadLine());
                        while (player.Bet > player.Wallet)
                        {
                            Console.WriteLine("You don't have enough to bet this much.");
                            Console.Write("Input: ");
                            player.Bet = Convert.ToInt32(Console.ReadLine());
                        }
                        gameStart(deck);

                        while (Card.playerTotal < 21 && Card.dealerTotal < 21 && dealerDone == false)
                        {
                            

                            Console.WriteLine("┌─────────────────────────────────────────────┐");
                            Console.WriteLine("│ Hit or stand?                               │");
                            Console.WriteLine("├─────────────────────────────────────────────┤");
                            Console.WriteLine("│                                             │");
                            Console.Write("│  (h) Hit                                    │\n");
                            Console.Write("│  (s) Stand                                  │\n");
                            Console.WriteLine("│                                             │");
                            Console.WriteLine("└─────────────────────────────────────────────┘");
                            Console.Write("Input: ");
                            string input = Console.ReadLine();

                            try
                            {
                                switch (input.ToUpper())
                                {
                                    case "H":
                                        Hit(deck);
                                        break;
                                    case "S":
                                        Stand(deck);
                                        break;
                                    default:
                                        Console.WriteLine("Invalid input");
                                        break;
                                }
                            }
                            catch (System.Exception)
                            {
                                Console.WriteLine("Invalid input");
                                throw;
                            }
                        }

                        string result = "";


                        switch (Card.playerTotal)
                        {
                            case var _ when Card.playerTotal == Card.dealerTotal:
                                Console.WriteLine($"Push! Draw");
                                result = "draw";
                                break;
                            case var _ when Card.playerTotal == 21 && Card.gameRound == 1:
                                Console.WriteLine($"Natural Blackjack! You win!");
                                result = "win";
                                break;
                            case var _ when Card.playerTotal < 21 && Card.playerTotal > Card.dealerTotal:
                                Console.WriteLine($"Player wins on score!");
                                result = "win";
                                break;
                            case 21:
                                Console.WriteLine($"Blackjack! You win!");
                                result = "win";
                                break;
                            case > 21:
                                Console.WriteLine($"Bust! You lose!");
                                result = "lose";
                                break;

                            default:
                                break;
                        }

                        switch (Card.dealerTotal)
                        {
                            case var _ when Card.playerTotal == Card.dealerTotal:
                                Console.WriteLine($"Push! Draw");
                                result = "draw";
                                break;
                            case var _ when Card.dealerTotal == 21 && Card.gameRound == 1:
                                Console.WriteLine($"Natural Blackjack! Dealer win!");
                                result = "loss";
                                break;
                            case var _ when Card.dealerTotal < 21 && Card.dealerTotal > Card.playerTotal:
                                Console.WriteLine($"Dealer wins on score!");
                                result = "loss";
                                break;
                            case 21:
                                Console.WriteLine($"Blackjack! Dealer win!");
                                result = "loss";
                                break;
                            case > 21:
                                Console.WriteLine($"Bust! Dealer loses!");
                                result = "win";
                                break;
                            default:
                                break;
                        }

                        player.GamesPlayed++;
                        player.totalBet += player.Bet;
                        decimal value = player.Bet;
                        

                        switch (result)
                        {
                            case "win":
                                value *= CasinoValues.multiplierBlackjackWin;
                                player.addMoney(value);
                                player.totalWon += value;
                                player.Wins++;
                                break;
                            case "lose":
                                player.subtractMoney(player.Bet);
                                player.totalWon -= player.Bet;
                                player.Losses++;
                                break;
                            case "draw":
                                value *= CasinoValues.multplierBlackjackDraw;
                                player.addMoney(player.Bet * CasinoValues.multplierBlackjackDraw);
                                player.Draws++;
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine($"┌─────────────────────────────────────────────────────┐");
                        Console.WriteLine($"│ Value won / Lost     : \u20AC{value,-19:N}         │");
                        Console.WriteLine($"│ Your current Balance : \u20AC{player.Wallet,-26:N}  │");
                        Console.WriteLine($"└─────────────────────────────────────────────────────┘");
                        break;

                    case 2:
                        player.Stats();
                        break;
                }
            }















            // Console.WriteLine(card.suitGen);
            // Console.WriteLine(card.faceGen);
            // Console.WriteLine(card.value);

            // string test = card.faceGen + " " + card.suitGen;
            // Console.WriteLine(test);
            // string test2 = card.fullCard;

            // TestSuit(); 
            // tests accuracy of random
            // averages 25% for each suit
            // range of -0.009% - +0.016% over 10m runs

            // TestFace();
            // tests random face gen
            // averages 7.69% (100/13) for each face




            // Card newCard = new Card();

            


            // System.Console.WriteLine(newCard.fullCard);

            // foreach (Card cards in deck)
            // {
            //     if (cards.fullCard == newCard.fullCard)
            //     {
            //             Console.WriteLine($"Duplicate: {cards.fullCard}");
            //             deck.Remove(cards);
            //             newCard = new Card();
            //             break;

            //     }
            //     else
            //     {
            //         Console.WriteLine($"Not a duplicate: {cards.fullCard}");
            //     }
            // }

            // System.Console.WriteLine($"total cards in deck: {deck.Count}");


















            // stores all 52 cards






        }


        static List<Card> initialize(List<Card> deck)
        {
            Card deckCards = new Card();
            List<Card> Deck = deckCards.DeckGen();
            Deck.Shuffle();

            return Deck;

        }
        static void gameStart(List<Card> deck)
        {
            Card dealer1 = new Card();
            removeFromDeck(deck, dealer1);
            Card dealer2 = new Card();
            if (dealer1.value + dealer2.value > 21)
            {
                dealer2 = new Card();
            }
            removeFromDeck(deck, dealer2);
            int dTotal = dealer1.value + dealer2.value;

            deck.Shuffle();

            Card player1 = new Card();
            removeFromDeck(deck, player1);
            Card player2 = new Card();
            player2.CardValue(Card.playerTotal);
            removeFromDeck(deck, player2);
            int pTotal = player1.value + player2.value;

            deck.Shuffle();


            Console.WriteLine("┌─────────────────────────────────────────────┐");
            Console.WriteLine("│ Dealer's Cards                              │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ {dealer1.faceGen,-8} of {dealer1.suitGen,-8} │ Value: {dealer1.value,-2}            │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ {dealer2.faceGen,-8} of {dealer2.suitGen,-8} │ Value: {dealer2.value,-2}            │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ Total: {dTotal,-36} │");
            Console.WriteLine("└─────────────────────────────────────────────┘");

            Console.WriteLine("┌─────────────────────────────────────────────┐");
            Console.WriteLine("│ Player's Cards                              │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ {player1.faceGen,-8} of {player1.suitGen,-8} │ Value: {player1.value,-2}            │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ {player2.faceGen,-8} of {player2.suitGen,-8} │ Value: {player2.value,-2}            │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ Total: {pTotal,-36} │");
            Console.WriteLine("└─────────────────────────────────────────────┘");

            // Console.WriteLine($"\nPlayer's Cards -------------------------------");
            // Console.WriteLine($"{player1.faceGen} of {player1.suitGen} Value : {player1.value}");
            // Console.WriteLine($"{player2.faceGen} of {player2.suitGen} Value : {player2.value}");
            // Console.WriteLine($"Total: {pTotal}");
            // Console.WriteLine($"----------------------------------------------");
            Card.playerTotal = pTotal;
            Card.dealerTotal = dTotal;
            Card.gameRound++;

            // Make playerTotal and dealerTotal static fields in the Program class


        }

        static void Hit(List<Card> deck)
        {
            deck.Shuffle();
            Card hitCard = new Card();
            hitCard.CardValue(Card.playerTotal);
            Card.playerTotal += hitCard.value;

            Console.WriteLine("┌─────────────────────────────────────────────┐");
            Console.WriteLine("│ Hit!                                        │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ {hitCard.faceGen,-8} of {hitCard.suitGen,-8} │ Value: {hitCard.value,-2}            │");
            Console.WriteLine("├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ New Total: {Card.playerTotal,-32} │");
            Console.WriteLine("└─────────────────────────────────────────────┘");

            removeFromDeck(deck, hitCard);
            Card.gameRound++;

        }

        static void Stand(List<Card> deck)
        {
            
            Console.WriteLine("┌─────────────────────────────────────────────┐");
            Console.WriteLine("│ You stood - Dealer's turn                   │");
            Console.WriteLine("└─────────────────────────────────────────────┘");

            while (Card.dealerTotal < 17)
            {
                deck.Shuffle();
                Card dealerCard = new Card();
                dealerCard.CardValue(Card.dealerTotal);
                Card.dealerTotal += dealerCard.value;

                Console.WriteLine("┌─────────────────────────────────────────────┐");
                Console.WriteLine("│ Hit!                                        │");
                Console.WriteLine("├─────────────────────────────────────────────┤");
                Console.WriteLine($"│ {dealerCard.faceGen,-8} of {dealerCard.suitGen,-8} │ Value: {dealerCard.value,-2}            │");
                Console.WriteLine("├─────────────────────────────────────────────┤");
                Console.WriteLine($"│ New Total: {Card.dealerTotal,-32} │");
                Console.WriteLine("└─────────────────────────────────────────────┘");
                Thread.Sleep(1000);

                removeFromDeck(deck, dealerCard);
                Card.gameRound++;
            }

            dealerDone = true;

        }

        static void removeFromDeck(List<Card> deck, Card inputCard)
        {
            foreach (Card cards in deck)
            {
                if (cards.fullCard == inputCard.fullCard)
                {
                    //Console.WriteLine($"Card is in deck {cards.fullCard}");
                    deck.Remove(cards);
                    inputCard = new Card();
                    break;
                }
                else if (cards.fullCard == inputCard.fullCard)
                {
                    //Console.WriteLine($"Card was already removed {cards.fullCard}");	
                }

            }
        }

        static void playerStart(List<Card> deck)
        {

        }

        static void TestSuit()
        {
            List<string> suits = new List<string>();
            int numTests = 100000;
            double total = 0;


            for (int i = 0; i < numTests; i++)
            {
                suits.Add(Card.GenerateSuit());
            }

            var groupedSuits = suits.GroupBy(x => x)
                                    .Select(g => new { Suit = g.Key, Count = g.Count() })
                                    .OrderByDescending(x => x.Count);

            foreach (var suit in groupedSuits)
            {
                double percentage = ((double)suit.Count / numTests) * 100;
                Console.WriteLine($"{suit.Suit}: {percentage}%");
                total += percentage;
            }

            System.Console.WriteLine($"Total: {total}%");

        }
        static void TestFace()
        {
            List<string> faces = new List<string>();
            int numTests = 10000000;
            double total = 0;

            for (int i = 0; i < numTests; i++)
            {
                faces.Add(Card.GenerateFace());
            }

            var groupedFaces = faces.GroupBy(x => x)
                                    .Select(g => new { Face = g.Key, Count = g.Count() })
                                    .OrderByDescending(x => x.Count);

            foreach (var face in groupedFaces)
            {
                double percentage = ((double)face.Count / numTests) * 100;
                Console.WriteLine($"{face.Face}: {percentage}%");
                total += percentage;
            }



        }
    }

    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    byte[] box = new byte[4];
                    rng.GetBytes(box);
                    int k = Math.Abs(BitConverter.ToInt32(box, 0)) % n;
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }
    }
}
