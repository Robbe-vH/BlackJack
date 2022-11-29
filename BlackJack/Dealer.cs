using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Dealer
    {
        public static int DealerPunten;
        public static int KaartScore;
        public static List<Kaart> dealerKaarten = new List<Kaart>();

        static Dealer()
        {
            DealerPunten = 0;
            KaartScore = 0;
        }
    }
}
