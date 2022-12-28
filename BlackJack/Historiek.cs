using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BlackJack
{
    public class Historiek
    {
        public ListBoxItem LbItLaatstGespeeldeHand = new ListBoxItem();
        private void NieuwItem()
        {
            LbItLaatstGespeeldeHand.Content = "Test";
        }

    }
}
