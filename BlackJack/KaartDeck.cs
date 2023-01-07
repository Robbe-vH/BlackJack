using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BlackJack
{
    /// <summary>
    /// Klasse die een <c>Kaart</c> beschrijft. Bevat publieke variabelen <c>string soort</c>, <c>string naam</c>, <c>BitmapImage Foto</c>.
    /// Bevat properties <c>string ImgSource</c> en <c>string Titel</c>.
    /// </summary>
    public class Kaart
    {
        public string soort = "";
        public string naam = "";
        public int waarde = 0;
        public BitmapImage Foto = new BitmapImage();
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


    /// <summary>
    /// Klasse met Lijst van <c>Kaart</c>.
    /// <para>Eigenschappen <c>soorten</c>, <c>namen</c> en <c>waardes</c>.</para>
    /// <para>Functies <c>Vuldeck</c>, <c>VerwijderUitDeck</c> en <c>GeefKaart</c>.</para>
    /// </summary>
    public static class KaartDeck
    {
        public static List<Kaart> deck = new List<Kaart>();
        private static string[] soorten = new string[4] { "Schuppen", "Klaveren", "Harten", "Ruiten" };
        private static string[] namen = new string[13] { "Aas", "Twee", "Drie", "Vier", "Vijf", "Zes", "Zeven", "Acht", "Negen", "Tien", "Boer", "Koningin", "Koning" };
        private static int[] waardes = new int[13] { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        private static Random rnd = new Random();

        static KaartDeck()
        {
            VulDeck();

        }

        /// <summary>
        /// 
        /// </summary>
        public static void VulDeck()
        {
            deck.Clear();
            // deck opvullen met kaart objecten

            for (int i = 0; i < 4; i++) // vier soorten
            {
                for (int j = 0; j < 13; j++) // dertien waardes
                {
                    //BitmapImage biFoto = new BitmapImage();
                    //biFoto.BeginInit();
                    //string imgpath = $"/Assets/{soorten[i]}{namen[j]}.png";
                    //biFoto.UriSource = new Uri(imgpath, UriKind.RelativeOrAbsolute);
                    //biFoto.EndInit();
                    deck.Add(new Kaart()
                    {
                        soort = soorten[i],
                        naam = namen[j],
                        ImgSource = $"/Assets/{soorten[i]}{namen[j]}.png",
                        waarde = waardes[j],
                        //Foto = biFoto,
                        Titel = $"{soorten[i]} {namen[j]}"
                    });
                }
            }
        }

        /// <summary>
        /// Haalt kaart uit Lijst <c>deck</c>
        /// <para>Vult Lijst <c>deck</c> opnieuw met Kaartobjecten <c>kaart</c> als <c>deck.Count == 0.</c></para>
        /// </summary>
        ///  <param name="deck">List van Kaart objecten</param>
        /// <param name="kaart"></param>
        private static void VerwijderUitDeck(List<Kaart> deck, Kaart kaart)
        {
            deck.Remove(kaart);
            if (deck.Count == 0)
            {
                MessageBox.Show("De kaarten worden opnieuw geschud!", "Kaarten Schudden", MessageBoxButton.OK);
                VulDeck();
            }
        }

        /// <summary>
        /// Maakt een tijdelijke <c>Kaart temp</c>, kiest daarna met een <c>Random</c> een kaart uit <c>deck</c>.
        /// <para>Als de <c>Kaart.Count</c> kleiner is dan 2, worden de kaarten opnieuw gevuld met functie </para>
        /// </summary>
        /// <param name="kaartscore">Outgoing parameter, geeft de score van de kaart terug als Int32.</param>
        /// <returns>1 kaartobject</returns>
        public static Kaart GeefKaart(out int kaartscore)
        {
            Kaart temp = new Kaart();

            if (deck.Count < 2)
            {
                MessageBox.Show("De kaarten worden opnieuw geschud!", "Kaarten Schudden", MessageBoxButton.OK);
                VulDeck();
            }

            int kaartTeller = rnd.Next(1, deck.Count - 1);
            temp = deck[kaartTeller];

            kaartscore = temp.waarde;
            VerwijderUitDeck(deck, deck[kaartTeller]);
            return temp;
        }
    }
}



