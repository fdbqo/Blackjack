using System;
using CardGen;
using System.Security.Cryptography;


namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {

            Card deckCards = new Card();
            List<Card> deck = deckCards.DeckGen();
            deck = initialize(deck);


            gameStart(deck);





            
            
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

            foreach (Card c in deck)
            {
                //Console.WriteLine(c.fullCard);
            }
            System.Console.WriteLine($"total cards in deck: {deck.Count}");


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
            removeFromDeck(deck, dealer2);
            int dTotal = dealer1.value + dealer2.value;

            Card player1 = new Card();
            removeFromDeck(deck, player1);
            Card player2 = new Card();
            removeFromDeck(deck, player2);
            int pTotal = player1.value + player2.value;

            Console.WriteLine($"\nDealer's Cards --------------");
            Console.WriteLine($"{dealer1.faceGen} of {dealer1.suitGen} Value : {dealer1.value}");
            Console.WriteLine($"{dealer2.faceGen} of {dealer2.suitGen} Value : {dealer2.value}");
            Console.WriteLine($"Total: {dTotal}");
            Console.WriteLine($"\nPlayer's Cards --------------");
            Console.WriteLine($"{player1.faceGen} of {player1.suitGen} Value : {player1.value}");
            Console.WriteLine($"{player2.faceGen} of {player2.suitGen} Value : {player2.value}");
      
        }

        static void removeFromDeck(List<Card> deck, Card inputCard)
        {
            foreach (Card cards in deck)
            {
                if (cards.fullCard == inputCard.fullCard)
                {
                        Console.WriteLine($"Duplicate: {cards.fullCard}");
                        deck.Remove(cards); 
                        inputCard = new Card();
                        break;
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
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
