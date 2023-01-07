using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Window voor de afgelopen 10 gespeelde handen. Geeft een Listbox die <c>Speler.historiekArray</c> afprint.
    /// </summary>
    public partial class HistoriekWindow : Window
    {
        public HistoriekWindow()
        {
            InitializeComponent();

            // empty the listbox
            LbxHistoriek.Items.Clear();

            // Populate listbox with array items
            for (int i = 0; i < Speler.historiekArray.Length; i++)
            {
                ListBoxItem temp = new ListBoxItem();
                temp.IsHitTestVisible = false;
                temp.Content = Speler.historiekArray[i];
                LbxHistoriek.Items.Add(temp);
            }
        }
    }
}
