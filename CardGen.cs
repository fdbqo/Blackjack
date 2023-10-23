using System.Security.Cryptography;

namespace CardGen
{
    class Card
    {
        public enum Suit
        {
            Clubs,
            Spades,
            Diamonds,
            Hearts
        }

        public enum Face
        {
            Ace,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten,
            Jack,
            Queen,
            King
        }
        public string suitGen { get; set; }
        public string faceGen { get; set; }
        public int value { get; set; }

        public Card()
        {
            faceGen = GenerateFace();
            suitGen = GenerateSuit();
            value = CardValue();
        }


        public static string GenerateSuit()
        {
            List<Suit> suits = new List<Suit> { Suit.Hearts, Suit.Diamonds, Suit.Clubs, Suit.Spades };
            int rand = RandomNumberGenerator.GetInt32(0, 4);
            // use rand to select suit from enum
            string resultSuit = suits[rand].ToString();
            return resultSuit;
        }

        public static string GenerateFace()
        {
            List<Face> faces = new List<Face> { Face.Ace, Face.Two, Face.Three, Face.Four, Face.Five, Face.Six, Face.Seven, Face.Eight, Face.Nine, Face.Ten, Face.Jack, Face.Queen, Face.King };
            Random rand = new Random();
            int index = rand.Next(faces.Count);
            string resultFace = faces[index].ToString();

            return resultFace;
        
        }

        public int CardValue()
        {
            string Face = GenerateFace();
            int value = 0;

            switch (Face)
            {
                case "Ace":
                    value = 11;
                    break;

                case "King":
                case "Queen":
                case "Jack":
                case "Ten":
                    value = 10;
                    break;

                // default:
                //     int parsedValue;
                //     if (int.TryParse(Face, out parsedValue))
                //     {
                //         value = parsedValue;
                //     }
                //     break;
            }

            return value;
        }

        public int CardValue(int currentScore)
        {
            string face = GenerateFace();
            int value = 0;

            switch (face)
            {
                case "Ace":
                    if (currentScore + 11 > 21)
                    {
                        value = 1;
                    }
                    else
                    {
                        value = 11;
                    }
                    break;

                case "King":
                case "Queen":
                case "Jack":
                case "Ten":
                    value = 10;
                    break;

                default:
                    value = int.Parse(face);
                    break;
            }

            return value;
        }

    }
}


