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
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public HistoriekWindow()
        {
            InitializeComponent();
            // lijst van listboxitems in lbx laden
            for (int i = 0; i < mainWindow.historiekArray.Length - 1; i++)
            {
                LbxHistoriek.Items.Add(mainWindow.historiekArray[i]);
            }
        }
    }
}
