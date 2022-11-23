using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    // Kaart klasse aanmaken
    public class Kaart
    {
        public string soort;
        public string naam;
        // public int kaartwaarde;

        public Kaart(string kaartSoort, string kaartNaam)
        {
            soort = kaartSoort;
            naam = kaartNaam;
        }

        public override string ToString()
        {
            return $"{soort} {naam}";
        }
    }


    // lijst van kaarten
    public static class KaartDeck
    {
        private static Kaart[] deck = new Kaart[53];
        private static string[] soorten = new string[4] { "Schuppen", "Klaveren", "Harten", "Ruiten" };
        private static string[] namen = new string[13] { "Aas", "Twee", "Drie", "Vier", "Vijf", "Zes", "Zeven", "Acht", "Negen", "Tien", "Boer", "Dame", "Koning" };
        private static Random rnd = new Random();
        private static Random rndSoort = new Random();

        static KaartDeck()
        {
            
        }

        public static void VulDeck()
        {
            // deck opvullen met kaart objecten
            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = new Kaart(soorten[rndSoort.Next(1,4)], namen[i / 13]); // aantal soorten is 4, dus de rest pakken van de deling voor het kleurnummer | 52/13 voor de naam van de kaart
            }
        }
        

        public static string GeefKaart()
        {
            VulDeck();
            int kaartTeller = rnd.Next(1,deck.Length);
            return Convert.ToString(deck[kaartTeller]);
        }
    }
}



