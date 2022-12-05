using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BlackJack
{
    // Kaart klasse aanmaken
    public class Kaart
    {
        public string soort = "";
        public string naam = "";
        public int waarde = 0;
        public string ImgSource { get; set; }
        public string Titel { get; set; }

        public Kaart()
        {
            ImgSource = string.Empty;
            Titel = string.Empty;
        }

        public override string ToString()
        {
            return $"{soort} {naam}";
        }
    }


    // lijst van kaarten
    public static class KaartDeck
    {
        private static List<Kaart> deck = new List<Kaart>();
        private static string[] soorten = new string[4] { "Schuppen", "Klaveren", "Harten", "Ruiten" };
        private static string[] namen = new string[13] { "Aas", "Twee", "Drie", "Vier", "Vijf", "Zes", "Zeven", "Acht", "Negen", "Tien", "Boer", "Dame", "Koning" };
        private static int[] waardes = new int[13] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        private static Random rnd = new Random();

        static KaartDeck()
        {
            VulDeck();

        }

        public static void VulDeck()
        {
            // deck opvullen met kaart objecten

            for (int i = 0; i < 4; i++) // vier soorten
            {
                for (int j = 0; j < 13; j++) // dertien waardes
                {
                    deck.Add(new Kaart()
                    {
                        soort = soorten[i],
                        naam = namen[j],
                        waarde = waardes[j],
                        ImgSource = $"Assets/{soorten[i]}{namen[j]}.png",
                        Titel = $"{soorten[i]} {namen[j]}"
                    });
                }
            }
        }

        public static Kaart GeefKaart(out int kaartscore)
        {
            int kaartTeller = rnd.Next(1, 52);

            kaartscore = deck[kaartTeller].waarde;
            return deck[kaartTeller];
        }
    }
}



