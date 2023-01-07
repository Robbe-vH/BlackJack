using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    /// <summary>
    /// Klasse voor de <c>Dealer</c>, bevat publieke variabelen voor <c>int DealerPunten</c>, <c>int KaartScore</c>, <c>int AantalAzen</c> en <c>List dealerKaarten</c>.
    /// </summary>
    public class Dealer
    {

        public static int DealerPunten;
        public static int KaartScore;
        public static int aantalAzen;
        public static List<Kaart> dealerKaarten = new List<Kaart>();

        static Dealer()
        {
            DealerPunten = 0;
            KaartScore = 0;
        }
    }
}
