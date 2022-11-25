using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Speler
    {
        public static int spelerPunten;
        public static int budget;
        public static int inzet;
        public static int KaartScore;

        public static string huidigeKaart;

        static Speler ()
        {
            spelerPunten = 0;
            budget = 0;
            inzet = 0;
            KaartScore = 0;
            huidigeKaart = "";
        }
    }
}
