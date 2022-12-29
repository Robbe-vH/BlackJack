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
    /// Interaction logic for HistoriekWindow.xaml
    /// </summary>
    public partial class HistoriekWindow : Window
    {
        public HistoriekWindow()
        {
            InitializeComponent();

            // empty the listbox
            LbxHistoriek.Items.Clear();

            // Populate listbox with array items
            for (int i = 0; i < Speler.historiekArray.Length - 1; i++)
            {
                ListBoxItem temp = new ListBoxItem();
                temp = Speler.historiekArray[i];
                LbxHistoriek.Items.Add(temp);
            }
        }
    }
}
