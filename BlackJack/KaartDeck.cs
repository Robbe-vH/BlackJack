using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BlackJack
{
    // Kaart klasse aanmaken
    public class Kaart
    {
        public string soort;
        public string naam;
        public int waarde;
        public Image foto;

        public Kaart(string kaartSoort, string kaartNaam, int kaartWaarde, Image img)
        {
            soort = kaartSoort;
            naam = kaartNaam;
            waarde = kaartWaarde;
            foto = img;
        }

        public override string ToString()
        {
            return $"{ soort } { naam }";
        }
    }


    // lijst van kaarten
    public static class KaartDeck
    {
        private static Kaart[] deck = new Kaart[53];
        private static string[] soorten = new string[4] { "Schuppen", "Klaveren", "Harten", "Ruiten" };
        private static string[] namen = new string[13] { "Aas", "Twee", "Drie", "Vier", "Vijf", "Zes", "Zeven", "Acht", "Negen", "Tien", "Boer", "Dame", "Koning" };
        private static Dictionary<int, Image> fotos = new Dictionary<int, Image>
        {
        1 = Image.SourceProperty("assets/HartenAas") 
        };
        private static int[] waardes = new int[13] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        private static Random rnd = new Random();

        static KaartDeck()
        {
            VulDeck();
        }

        public static void VulDeck()
        {
            // deck opvullen met kaart objecten
            for (int i = 0; i < deck.Length; i++)
            {
                int soort = rnd.Next(1, 4);
                int waarde = rnd.Next(1,13);
                deck[i] = new Kaart(soorten[soort], namen[waarde], waardes[waarde], fotos[i]); // aantal soorten is 4, 52/13 voor de naam van de kaart en de score
            }
        }
        

        public static string GeefKaart(out int kaartscore)
        {
            int kaartTeller = rnd.Next(1,deck.Length);

            kaartscore = deck[kaartTeller].waarde;
            return deck[kaartTeller].ToString();
        }
    }
}



