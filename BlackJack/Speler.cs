using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BlackJack
{
    /// <summary>
    /// Klasse voor de <c>Dealer</c>, bevat publieke variabelen voor <c>int DealerPunten</c>, <c>int KaartScore</c>, <c>int AantalAzen</c> en <c>List dealerKaarten</c>.
    /// </summary>
    public class Speler
    {
        public static int SpelerPunten;
        public static int Budget;
        public static int Inzet;
        public static int KaartScore;
        public static int aantalAzen;
        public static string[] historiekArray = new string[10];

        public static List<Kaart> spelerKaarten = new List<Kaart>();

        static Speler ()
        {
            SpelerPunten = 0;
            Budget = 0;
            Inzet = 0;
            KaartScore = 0;
        }
    }
}
