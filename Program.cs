using System;
using CardGen;

using CardGen;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {

            Card card = new Card();

            Console.WriteLine(card.suitGen);
            Console.WriteLine(card.faceGen);
            Console.WriteLine(card.value);

            // TestSuit(); 
            // tests accuracy of random
            // averages 25% for each suit
            // range of -0.009% - +0.016% over 10m runs

            // TestFace();
            // tests random face gen
            // averages 7.69% (100/13) for each face


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
}
