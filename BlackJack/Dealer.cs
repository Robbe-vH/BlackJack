using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Dealer
    {
        public static int dealerPunten;
        public static int KaartScore;
        public static string huidigeKaart;

        static Dealer()
        {
            dealerPunten = 0;
            KaartScore = 0;
            huidigeKaart = "";
        }
    }
}
