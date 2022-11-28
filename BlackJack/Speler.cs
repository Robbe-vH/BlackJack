using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Speler
    {
        public static int SpelerPunten;
        public static int Budget;
        public static int Inzet;
        public static int KaartScore;

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
